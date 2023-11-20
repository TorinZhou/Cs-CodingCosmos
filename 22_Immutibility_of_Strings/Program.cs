string text = "abc";
text += "d";


//! all string is immutible. modify them return a new string
var upperCase = text.ToUpper();
Console.WriteLine(text);
Console.WriteLine(upperCase);


Console.ReadKey();