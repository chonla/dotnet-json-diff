using System;
using Xunit;

namespace JsonDiff.Tests
{
    public class JsonDiffTest
    {
        private JsonDiff diff;

        public JsonDiffTest() {
            this.diff = new JsonDiff();
        }

        [Fact]
        public void FlatIdenticalComparisonTest()
        {
            var obj1 = new {
                a = 1
            };
            var obj2 = new {
                a = 1
            };

            var result = this.diff.Compare(obj1, obj2);

            Assert.Equal(0, result.Count());
        }

        [Fact]
        public void Flat1DifferenceComparisonTest()
        {
            var obj1 = new {
                a = 1
            };
            var obj2 = new {
                a = 2
            };

            var result = this.diff.Compare(obj1, obj2);

            Assert.Equal(1, result.Count());
            Assert.Equal("updated", result.Get("a"));
        }

        [Fact]
        public void Flat2DifferenceComparisonTest()
        {
            var obj1 = new {
                a = 1,
                b = 2
            };
            var obj2 = new {
                a = 2,
                b = 1
            };

            var result = this.diff.Compare(obj1, obj2);

            Assert.Equal(2, result.Count());
            Assert.Equal("updated", result.Get("a"));
            Assert.Equal("updated", result.Get("b"));
        }

        [Fact]
        public void FlatPartialDifferenceComparisonTest()
        {
            var obj1 = new {
                a = 1,
                b = 2
            };
            var obj2 = new {
                a = 1,
                b = 1
            };

            var result = this.diff.Compare(obj1, obj2);

            Assert.Equal(1, result.Count());
            Assert.Equal("updated", result.Get("b"));
        }

        [Fact]
        public void FlatAddedDifferenceComparisonTest()
        {
            var obj1 = new {
                a = 1
            };
            var obj2 = new {
                a = 1,
                b = 1
            };

            var result = this.diff.Compare(obj1, obj2);

            Assert.Equal(1, result.Count());
            Assert.Equal("added", result.Get("b"));
        }

        [Fact]
        public void FlatDeletedDifferenceComparisonTest()
        {
            var obj1 = new {
                a = 1,
                b = 1
            };
            var obj2 = new {
                a = 1
            };

            var result = this.diff.Compare(obj1, obj2);

            Assert.Equal(1, result.Count());
            Assert.Equal("deleted", result.Get("b"));
        }

        [Fact]
        public void NestedIdenticalComparisonTest()
        {
            var obj1 = new {
                a = 1,
                b = new {
                    c = 1,
                    d = 2
                }
            };
            var obj2 = new {
                a = 1,
                b = new {
                    c = 1,
                    d = 2
                }
            };

            var result = this.diff.Compare(obj1, obj2);

            Assert.Equal(0, result.Count());
        }

        [Fact]
        public void NestedAddedDifferenceComparisonTest()
        {
            var obj1 = new {
                a = 1,
                b = new {
                    c = 1,
                    d = 2
                }
            };
            var obj2 = new {
                a = 1,
                b = new {
                    c = 1,
                    d = 2,
                    e = 3
                }
            };

            var result = this.diff.Compare(obj1, obj2);

            Assert.Equal(2, result.Count());
            Assert.Equal("updated", result.Get("b"));
            Assert.Equal("added", result.Get("b.e"));
        }

        [Fact]
        public void NestedDeletedDifferenceComparisonTest()
        {
            var obj1 = new {
                a = 1,
                b = new {
                    c = 1,
                    d = 2,
                    e = 3
                }
            };
            var obj2 = new {
                a = 1,
                b = new {
                    c = 1,
                    d = 2
                }
            };

            var result = this.diff.Compare(obj1, obj2);

            Assert.Equal(2, result.Count());
            Assert.Equal("updated", result.Get("b"));
            Assert.Equal("deleted", result.Get("b.e"));
        }

        [Fact]
        public void NestedUpdatedDifferenceComparisonTest()
        {
            var obj1 = new {
                a = 1,
                b = new {
                    c = 1,
                    d = 2,
                    e = 3
                }
            };
            var obj2 = new {
                a = 1,
                b = new {
                    c = 1,
                    d = 2,
                    e = 4
                }
            };

            var result = this.diff.Compare(obj1, obj2);

            Assert.Equal(2, result.Count());
            Assert.Equal("updated", result.Get("b"));
            Assert.Equal("updated", result.Get("b.e"));
        }

        [Fact]
        public void NestedNestAddedDifferenceComparisonTest()
        {
            var obj1 = new {
                a = 1,
                b = new {
                    c = 1,
                    d = 2,
                    e = new {
                        f = 1,
                        g = 2,
                        h = new {
                            i = 1,
                            j = 2,
                            k = 3,
                            l = 4,
                            m = 5
                        }
                    }
                }
            };
            var obj2 = new {
                a = 1,
                b = new {
                    c = 1,
                    d = 2,
                    e = new {
                        f = 1,
                        g = 2,
                        h = new {
                            i = 1,
                            j = 2,
                            k = 3,
                            l = 4,
                            m = 5,
                            n = 6
                        }
                    }
                }
            };

            var result = this.diff.Compare(obj1, obj2);

            Assert.Equal(4, result.Count());
            Assert.Equal("updated", result.Get("b"));
            Assert.Equal("updated", result.Get("b.e"));
            Assert.Equal("updated", result.Get("b.e.h"));
            Assert.Equal("added", result.Get("b.e.h.n"));
        }

        [Fact]
        public void NestedNestJsonStringComparisonTest()
        {
            var json1 = @"{
                ""a"": 1,
                ""b"": {
                    ""c"": 1,
                    ""d"": 2,
                    ""e"": {
                        ""f"": 1,
                        ""g"": 2,
                        ""h"": {
                            ""i"": 1,
                            ""j"": 2,
                            ""k"": 3,
                            ""l"": 4,
                            ""m"": 5
                        }
                    }
                }
            }";
            var json2 = @"{
                ""a"": 1,
                ""b"": {
                    ""c"": 1,
                    ""d"": 2,
                    ""e"": {
                        ""f"": 1,
                        ""g"": 2,
                        ""h"": {
                            ""i"": 1,
                            ""j"": 2,
                            ""k"": 3,
                            ""l"": 4,
                            ""m"": 5,
                            ""n"": 6
                        }
                    }
                }
            }";

            var result = this.diff.Compare(json1, json2);

            Assert.Equal(4, result.Count());
            Assert.Equal("updated", result.Get("b"));
            Assert.Equal("updated", result.Get("b.e"));
            Assert.Equal("updated", result.Get("b.e.h"));
            Assert.Equal("added", result.Get("b.e.h.n"));
        }
    }
}
