

var array = new int[] { 1, 2, 3 };
//array.Add(1);
//! wouldn't compile
ICollection<int> arrayAsCollection = array;
arrayAsCollection.Add(1);
//! cause runtime error
var implementedInterfaces = array.GetType().GetInterfaces();




Console.ReadKey();
