using System.IO;
using System.Text;
using CustomDoublyLinkedListLibrary;

namespace USATU_OOP_LW_7;

public abstract class GraphicObjectsListAbstractFactory
{
    private const string CountPrefix = "Count: ";

    public abstract CustomDoublyLinkedList<GraphicObject> ParseGraphicObjects(StringReader dataStringReader);

    public string PrepareDataToStore(CustomDoublyLinkedList<GraphicObject> graphicObjects)
    {
        var allDataBuilder = new StringBuilder();
        allDataBuilder.AppendLine(CountPrefix + graphicObjects.Count);
        allDataBuilder.AppendLine();

        var objectsDataBuilder = new StringBuilder();
        for (var i = graphicObjects.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
        {
            objectsDataBuilder.AppendLine(i.Current.PrepareDataToStore());
        }

        objectsDataBuilder.Replace("\n", "\n\t");
        allDataBuilder.Append(objectsDataBuilder);
        return allDataBuilder.ToString();
    }
}