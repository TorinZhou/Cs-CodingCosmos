// Being able to know when things might or might not be null, 
// even when it's only to 90% confidence, is a lot better than 0% confidence ~Jon Skeet

// from .net 8
// if a type is declared as not nullable. the compiler will warn us

string nonNullableString = null; // notice we get a warning here
string? nullableString = null;

// nullable ref types do not change how the code works.
// it just change weaher the compiler warn us. 
// good thing to have for IDEs and code review!


Console.WriteLine(nullableString);
Console.ReadKey();