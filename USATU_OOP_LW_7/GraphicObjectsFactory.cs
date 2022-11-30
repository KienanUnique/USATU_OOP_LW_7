using System;
using System.IO;

namespace USATU_OOP_LW_7;

public class GraphicObjectsFactory : GraphicObjectsAbstractFactory
{
    public override GraphicObject ParseGraphicObject(string typeOfObject)
    {
        Enum.TryParse(typeOfObject, out GraphicObjectsTypes objectType);
        GraphicObject newGraphicObject = objectType switch
        {
            GraphicObjectsTypes.Group => new GraphicObjectGroup(this),
            GraphicObjectsTypes.Circle => new Circle(),
            GraphicObjectsTypes.Square => new Square(),
            GraphicObjectsTypes.Triangle => new Triangle(),
            GraphicObjectsTypes.Pentagon => new Pentagon(),
            _ => null
        };

        return newGraphicObject;
    }
}