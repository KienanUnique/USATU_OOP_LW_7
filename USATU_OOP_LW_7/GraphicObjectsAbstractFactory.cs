using System.IO;

namespace USATU_OOP_LW_7;

public abstract class GraphicObjectsAbstractFactory
{
    public abstract GraphicObject ParseGraphicObject(string typeOfObject);
}