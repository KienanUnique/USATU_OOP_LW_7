using System;
using System.IO;

namespace USATU_OOP_LW_7;

public class GraphicObjectsList : GraphicObjectsListAbstractFactory
{
    public GraphicObjectsList() : base()
    {
    }

    public GraphicObjectsList(StringReader dataStringReader) : base()
    {
        int.TryParse(dataStringReader.ReadLine(), out int countOfElements);

        for (int i = 0; i < countOfElements; i++)
        {
            Enum.TryParse(dataStringReader.ReadLine(), out GraphicObjectsTypes objectType);
            switch (objectType)
            {
                case GraphicObjectsTypes.Group:
                    var newGroup = new GraphicObjectGroup(dataStringReader);
                    Add(newGroup);
                    break;
                case GraphicObjectsTypes.Figure:
                    Enum.TryParse(dataStringReader.ReadLine(), out Figures figureType);
                    Figure newFigure = figureType switch
                    {
                        Figures.Circle => new Circle(dataStringReader),
                        Figures.Square => new Square(dataStringReader),
                        Figures.Triangle => new Triangle(dataStringReader),
                        Figures.Pentagon => new Pentagon(dataStringReader),
                        _ => null
                    };

                    Add(newFigure);
                    break;
            }
        }
    }
}