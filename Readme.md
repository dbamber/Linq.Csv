# Linq.Csv

# Install

```
Install-Package Linq.Csv 
```

# Usage

Add the following using statement which will allow you to use the 

```
using Linq.Csv;
```

Then you can use any of the following api

## With Header
```
string csvContent = Enumerable.Range(1, 50).Csv(new string[] { "Number" }, p => p);
```

## Without Header
```
string csvContent = Enumerable.Range(1, 50).Csv(p => p);
```

## To Stream
You can also write directly to the stream.

```
Stream file = File.OpenWrite("output");
file.WriteCsv(Enumerable.Range(1, 50), p => p, p => p * p);
```