using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonDiff
{
    public class JsonDiff
    {
        public Dictionary<string, object> Parse(object obj) {
            var json = JsonConvert.SerializeObject(obj);
            var dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            return dict;
        }

        public JsonDiffResult Compare(string json1, string json2) {
            var obj1 = JsonConvert.DeserializeObject(json1);
            var obj2 = JsonConvert.DeserializeObject(json2);
            return this.Compare(obj1, obj2);
        }

        public JsonDiffResult Compare(object obj1, object obj2) {
            var d1 = this.Parse(obj1);
            var d2 = this.Parse(obj2);
            var result = new JsonDiffResult();

            if (d1.Equals(d2)) {
                return result;
            }

            foreach (var o in d1) {
                if (d2.ContainsKey(o.Key)) {
                    if (!o.Value.Equals(d2[o.Key])) {
                        if (o.Value.GetType() == typeof(JObject)) {
                            var subResult = this.Compare(o.Value, d2[o.Key]);
                            if (subResult.Count() > 0) {
                                result.Add(o.Key, "updated");
                                result.Merge(o.Key, subResult);
                            }
                        } else {
                            result.Add(o.Key, "updated");
                        }
                    }
                } else {
                    result.Add(o.Key, "deleted");
                }
            }

            foreach (var o in d2) {
                if (!d1.ContainsKey(o.Key)) {
                    result.Add(o.Key, "added");
                }
            }

            return result;
        }
    }
}
