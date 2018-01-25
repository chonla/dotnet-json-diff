using System;
using System.Collections.Generic;

namespace JsonDiff {
    public class JsonDiffResult {
        private Dictionary<string, string> result;
        private List<string> keys;
        
        public JsonDiffResult() {
            this.result = new Dictionary<string, string>();
            this.keys = new List<string>();
        }

        public int Count() {
            return this.result.Count;
        }

        public string Get(string key) {
            return this.result[key];
        }

        public string[] Keys() {
            return this.keys.ToArray();
        }

        public void Add (string key, string reason) {
            this.result.Add(key, reason);
            this.keys.Add(key);
        }

        public void Merge(string prefix, JsonDiffResult o) {
            var k = o.Keys();
            var c = k.Length;
            for (var i = 0; i < c; i++) {
                var newKey = prefix + "." + k[i];
                this.Add(newKey, o.Get(k[i]));
            }
        }
    }
}