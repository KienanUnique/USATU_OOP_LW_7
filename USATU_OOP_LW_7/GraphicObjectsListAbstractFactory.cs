using System.Text;
using CustomDoublyLinkedListLibrary;

namespace USATU_OOP_LW_7;

public abstract class GraphicObjectsListAbstractFactory : CustomDoublyLinkedList<GraphicObject>, IStorableObject
{
    private const string CountPrefix = "Count: ";

    public string GetDataToStore()
    {
        var allDataBuilder = new StringBuilder();
        allDataBuilder.AppendLine(CountPrefix + Count);
        allDataBuilder.AppendLine();

        var objectsDataBuilder = new StringBuilder();
        for (var i = GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
        {
            objectsDataBuilder.AppendLine(i.Current.GetDataToStore());
        }

        objectsDataBuilder.Replace("\n", "\n\t");
        allDataBuilder.Append(objectsDataBuilder);
        return allDataBuilder.ToString();
    }
}