//int number = null;

// null represents a lack of value or missing value.
// sometimes this null should be appled to value types. 
// for example: age of an individual. You don't want to do -1 as a walkaround

string text = null;
int? numberOrNull = null;
Nullable<bool> boolOrNull = null;

// the "?" mark here, is just a syntax sugar for the Nullable type
// (its a struct, as all value types, its a value type)
// here's the problem, if we can't assign null to value types
// why we can assign null to the value struct?
// actually, it's not assigning null. the compiler translate the code to 
// Nullable<int> numberOrNull = new Nullable<int>() 
// as you can see, the compiler doesn't assign anything, just a new struct.

if (numberOrNull.HasValue)
{
    int number = numberOrNull.Value;
    Console.WriteLine("not null");
}
if (boolOrNull is not null)
{
    Console.WriteLine(boolOrNull.Value);
}

// let's wrap up
var ages = new List<Nullable<int>>
{
    18, null, 19, null, 30, 20, null
};

// notice here: the Average() is smart enough to not include null
// so I don't need to check whatsoever. ther Where() can be dropped
var averageAge = ages
    .Where(age => age is not null)
    .Average();

Console.WriteLine(averageAge);


Console.ReadKey();

