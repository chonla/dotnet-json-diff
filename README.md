# JsonDiff

Find differences in 2 Json Strings/Objects.

## Usage

```
var diff = new JsonDiff();
var result = diff.Compare(json1, json2);
```

## Return

*JsonDiffResult* object

## JsonDiffResult

JsonDiffResult contains a map of difference of 2 Jsons. Key is an id to Json property. Nested key is delimited by a dot. Value is a string indicating how different between 2 Jsons -- *added*, *deleted* or *updated*.

## JsonDiffResult Methods

| Method | Arguments | Description |
|--------|-----------|-------------|
| Count  | - | Returns number of difference. |
| Get    | key | Returns difference item from given key. |
| Keys   | - | Returns key list. |

## Examples

Difference of 2 Json objects

```
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

/*
{
    "b": "updated",
    "b.e": "added"
}
*/
```

Difference of 2 Json string

```
var json1 = @"{
    ""a"": 1,
    ""b"": {
        ""c"": 1,
        ""d"": 2
    }
}";
var json2 = @"{
    ""a"": 1,
    ""b"": {
        ""c"": 1,
        ""d"": 2,
        ""e"": 3
    }
}";

var result = this.diff.Compare(json1, json2);

/*
{
    "b": "updated",
    "b.e": "added"
}
*/
```

## License

MIT: http://chonla.mit-license.org/