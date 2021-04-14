using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Tekla.Structures.Model;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model.UI;
        
namespace myspace
{
    class Myclass
    {        
        static void Main()
        {
            colorizeParts("121,123,126");
        }
        
        static void colorizeParts(string Phasestring)
        {   //Will apply colour to parts in Tekla model
            //
            List<ModelObject> visibleParts = new List<ModelObject>();
            visibleParts = getPartsFromView(Phasestring); //or you can choose other method to get parts from Tekla model
            MessageBox.Show($"Objects in phase: {Convert.ToString(visibleParts.Count)}");
            ModelObjectVisualization.SetTemporaryState(visibleParts, new Color(1, 0, 0));
        }
        
        
        //fiew ways to select a parts from Tekla model:
        
        static List<ModelObject> getAllPartsWithType()
        {  
           Model Model = new Model();
           System.Type[] Types = new System.Type[2];
           Types.SetValue(typeof(Part),0);
           Types.SetValue(typeof(Tekla.Structures.Model.Boolean),1);
           ModelObjectEnumerator myEnum = Model.GetModelObjectSelector().GetAllObjectsWithType(Types);
           List<ModelObject> visibleParts = new List<ModelObject>();
           while (myEnum.MoveNext())
           {    visibleParts.Add(myEnum.Current);}
           return visibleParts;
        }
        
        static List<ModelObject> getSelectedPartsFromView()
        {   
            List<ModelObject> visibleParts = new List<ModelObject>();    
            ModelObjectEnumerator selectedObjects = new Tekla.Structures.Model.UI.ModelObjectSelector().GetSelectedObjects();
            while (selectedObjects.MoveNext())
            {  if (selectedObjects.Current is Tekla.Structures.Model.Part)  { visibleParts.Add(selectedObjects.Current); }
            }
            MessageBox.Show($"Objects on the view: {Convert.ToString(visibleParts.Count)}");
            return visibleParts;
        }
        
        static List<ModelObject> getPartsFromActiveView(string PhaseString) //with Phase filter
        {   List<ModelObject> visibleParts = new List<ModelObject>();
            Tekla.Structures.Model.UI.ModelViewEnumerator views = Tekla.Structures.Model.UI.ViewHandler.GetVisibleViews();
            Point min = new Point(-999999, -999999, -999999);
            Point max = new Point(999999, 999999, 999999);
            Tekla.Structures.Model.UI.View view = new Tekla.Structures.Model.UI.View();
            while (views.MoveNext()) {view = views.Current;}
            Tekla.Structures.Model.UI.ModelObjectSelector selector = new Tekla.Structures.Model.UI.ModelObjectSelector();
            Tekla.Structures.Model.ModelObjectEnumerator allObjects = selector.GetObjectsByBoundingBox(min, max, view);
            if(PhaseString == "0") 
            {   while(allObjects.MoveNext())
                {  if(allObjects.Current is Tekla.Structures.Model.Part)     
                    {   visibleParts.Add(allObjects.Current);  }
                }
            }
            else
            {   while (allObjects.MoveNext())
                {  if (allObjects.Current is Tekla.Structures.Model.Part)     
                    {   int phaseNumber = 0;
                        allObjects.Current.GetReportProperty("PHASE", ref phaseNumber);
                        if(PhaseString.Contains(Convert.ToString(phaseNumber))) {visibleParts.Add(allObjects.Current); } 
                    }
                }
            }
            return visibleParts;
        }
    }
}        
