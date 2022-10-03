using KompasAPI7;
using Kompas6API5;
using Kompas6Constants3D;
using Kompas6Constants;
using KAPITypes;

using System;
using System.IO;
using Microsoft.Win32;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace CoreBarrelsApp
{
	public class KompasModel
	{
        public static void Compile(Dictionary <string, double> kompasValues)
        {
            Type t = Type.GetTypeFromProgID("KOMPAS.Application.5");
            KompasObject kompas = (KompasObject)Activator.CreateInstance(t);

            kompas.Visible = true;
            kompas.ActivateControllerAPI();

            DateTime dateTime = DateTime.Now.ToLocalTime(); 
            string basefilePath = Path.GetFullPath("kompas\\керноприемник.a3d");
            string newFilePath = Path.GetFullPath("kompas\\керноприемник-"+ dateTime.ToString("dd-MM-yyyy-HH-mm-ss-fff") + ".a3d");
            File.Copy(basefilePath, newFilePath, true);
            ksDocument3D kompas_document_3d = (ksDocument3D)kompas.Document3D();
            kompas_document_3d.Open(newFilePath);

            for (int i = 0; i < 10; i++)
            {
                ksPart part = (ksPart)kompas_document_3d.GetPart(i);

                ksVariableCollection varCol = (ksVariableCollection)part.VariableCollection();
                ksVariable paramItem = (ksVariable)kompas.GetParamStruct((short)StructType2DEnum.ko_VariableParam);
                for (int j = 0; j < varCol.GetCount(); j++)
                {
                    paramItem = (ksVariable)varCol.GetByIndex(j);
                    //var message = string.Format("Номер переменной {0}-{1}\nИмя переменной {2}\nЗначение переменной {3}\nКомментарий {3}", i, j, paramItem.name, paramItem.value, paramItem.note);
                    //kompas.ksMessage(paramItem.name);
                    if (kompasValues.ContainsKey("TB__" + paramItem.name)) paramItem.value = kompasValues["TB__" + paramItem.name];
                }
                part.RebuildModel();
            }
        }
	}
}

