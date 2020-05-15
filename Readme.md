# Input format

```
UserName: super_user;
Password: top_secret_password;

TimeToLive: 10;
IsEnabled: false;
```

# Assumption
* A key name, colon then value
* Key names are case sensitive
* A key value can be a Boolean( true, false), an integer (e.g. 1, -2, 4) or a string
* All values that are not Boolean or integer should be treated as strings
* Integer values can be safely stored in int type. Integer values wil be written as a series of digits, with an additional minus sign at the begenning for negative numbers
* A configuration file can contain empty lines.
* Neither a key name nor a value will contain a colon.
* Each line in a configuration file, except for empty lines will have a semicolon at the end


## Specification
* Every key name found in a config file should be exposed as a property of an object retured from a parser.

```
var r = parser.Parse(s)
Console.Writeline(r.TimeToLive);
```

* These properties should be of an appropriate type i.e. bool, int or string
* Parser should throw ArgumentExcption if input string is null or empty
* Keys and Values should have whitespace trimmed.
* If non existent property is read, UnknownKeyException should be thrown
* If a key name is null or empty, EmptyKeyException should be thrown
* If a key is found to be invalid C# Property Name, InvalidKeyException should be thrown.
* If duplicates are detected, DuplicatedKeyException should be thrown.