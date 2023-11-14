
//bool Contains<T>(T itemToCheck, IEnumerable<T> input)
//{
//    foreach (var item in input)
//    {
//        if(item.Equals(itemToCheck))
//        {
//            return true;
//        }
//    } 
//    return false;
//}


var sortedList = new List<int>
{
    1,2,4,5,6,7,8,9,11,23,33,44,55,66,
};
var indexOf1 = sortedList.FindIndexInSorted(9);
var indexOf2 = sortedList.FindIndexInSorted(1);
var indexOf3 = sortedList.FindIndexInSorted(66);
var indexOf4 = sortedList.FindIndexInSorted(55);
var indexOf5 = sortedList.FindIndexInSorted(6666);

Console.ReadKey();
//! ListExtensions.FindIndexInSorted(myList, 3)
public static class ListExtensions
    //! extends the IList 
{

    public static int? FindIndexInSorted<T>(this IList<T> list, T itemToFind)
        where T : IComparable<T>
    {
        int leftBound = 0;
        int rightBound = list.Count - 1;
        while (leftBound <= rightBound)
        {
            int middleIndex = (leftBound + rightBound) / 2;
            if (itemToFind.Equals(list[middleIndex]))
            {
                return middleIndex;
            }
            else if(itemToFind.CompareTo(list[middleIndex]) < 0)
            {
                rightBound = middleIndex - 1;
            }
            else
            {
                leftBound = middleIndex + 1;
            }

        }
        return null;

    }
}