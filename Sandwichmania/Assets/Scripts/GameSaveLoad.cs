﻿using UnityEngine; 
using System.Collections;
using System.Collections.Generic;
using System.Xml; 
using System.Xml.Serialization; 
using System.IO; 
using System.Text; 

public static class GameSaveLoad {

	//dirección a la que se enviarán los resultados de la partida para ser insertados a la base de datos.
	//static string _postURL3 = "http://132.248.16.11/unity/insertaPenal.php"; 
	public static string _PacienteName; 
	public static string _PacienteNameWithSpaces;
	public static string _FileLocation="C:\\LANR\\Sandwich"; //Dirección donde se van a almacenar los XML
	static string _data; //esta cadena se usará para almacenar toda la información de los resultados del paciente

	//*************************************************** 
	// Saving  
	// **************************************************    
	public static void Save(InfoPartida myData){ 
		//Cadena donde almacenamos la información de myData
		_data = SerializeObject(myData); 
		//Con la info de _data creamos el XML 
		CreateXML(); 
	} 

	//*************************************************** 
	// Loading
	// **************************************************       
	public static InfoPartida Load(){ 
		//Creamos el objeto de tipo PacienteData donde almacenaremos la info deserializada de _data
		InfoPartida myData2 = new InfoPartida ();
		//La información del XML la guardamos en la cadena _data
		LoadXML(); 
		if(_data.ToString() != "") 
		{ 
			// notice how I use a reference to type (PacienteData) here, you need this 
			// so that the returned object is converted into the correct type 
			myData2 = (InfoPartida)DeserializeObject(_data); 
		} 
		return myData2;
	} 

	//sobrecarga del método anterior
	public static InfoPartida Load(string filePath){  
		InfoPartida myData2 = new InfoPartida ();
		LoadXML(filePath); 
		if(_data.ToString() != "") 
		{ 
			myData2 = (InfoPartida)DeserializeObject(_data); 
		} 
		return myData2;
	} 


	//Las siguientes funciones seriven para deserializar y serializar cadenas de información, lo mejor es no modificarlas salvo que el tipo de dato que se quiera transformar sea deiferente de PacienteData
	//------------------------------------------------------------------------------------------------------------------
	//------------------------------------------------------------------------------------------------------------------
	static	string UTF8ByteArrayToString(byte[] characters) 
	{      
		UTF8Encoding encoding = new UTF8Encoding(); 
		string constructedString = encoding.GetString(characters); 
		return (constructedString); 
	} 

	public static byte[] StringToUTF8ByteArray(string pXmlString) 
	{ 
		UTF8Encoding encoding = new UTF8Encoding(); 
		byte[] byteArray = encoding.GetBytes(pXmlString); 
		return byteArray; 
	} 

	// Here we serialize our PacienteData object of myData 
	static string SerializeObject(object pObject) 
	{ 
		string XmlizedString = null; 
		MemoryStream memoryStream = new MemoryStream(); 
		XmlSerializer xs = new XmlSerializer(typeof(InfoPartida)); 
		XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8); 
		xs.Serialize(xmlTextWriter, pObject); 
		memoryStream = (MemoryStream)xmlTextWriter.BaseStream; 
		XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray()); 
		return XmlizedString; 
	} 

	// Here we deserialize it back into its original form 
	static object DeserializeObject(string pXmlizedString) 
	{ 
		XmlSerializer xs = new XmlSerializer(typeof(InfoPartida)); 
		MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(pXmlizedString)); 
		XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8); 
		return xs.Deserialize(memoryStream); 
	} 

	//------------------------------------------------------------------------------------------------------------------
	//------------------------------------------------------------------------------------------------------------------

	//Se crea el archivo XML que contiene la información de los resultados de la partida que jugó el paciente
	//En caso de que ya exista un archivo XML que contenga información del paciente, se almacenará dicha información y se concatenará al final de los nuevos resultados
	//El archivo original será borrado y se creará uno nuevo que contendra la nueva y la vieja información
	static void CreateXML() 
	{ 
		StreamWriter writer; 
		FileInfo t = new FileInfo(_FileLocation+"\\"+_PacienteName+"_DataSandwich.xml"); 
		if(!t.Exists) 
		{ 
			writer = t.CreateText(); 
		} 
		else   
		{ 
			InfoPartida myFinalData = new InfoPartida ();
			InfoPartida myNewData = new InfoPartida ();
			//primero cargamos la informacion que ya existia en el archivo
			StreamReader r = File.OpenText(_FileLocation+"\\"+_PacienteName+"_DataSandwich.xml"); 
			string _oldData = r.ReadToEnd(); 
			r.Close();  
			t.Delete(); 
			myFinalData=(InfoPartida)DeserializeObject(_oldData);
			myNewData=(InfoPartida)DeserializeObject(_data);
			//agregar todas las partidas de la sesion
			for(int i=0;i<myNewData.HistorialPartidasNivel1.Count;i++){
				myFinalData.HistorialPartidasNivel1.Add(myNewData.HistorialPartidasNivel1[i]);
			}
			//agregar todo el historial de partidas personalizadas
			for(int i=0;i<myNewData.HistorialPartidasNivel2.Count;i++){
				myFinalData.HistorialPartidasNivel2.Add(myNewData.HistorialPartidasNivel2[i]);
			}

			for(int i=0;i<myNewData.HistorialPartidasNivel3.Count;i++){
				myFinalData.HistorialPartidasNivel3.Add(myNewData.HistorialPartidasNivel3[i]);
			}

			_data="";
			_data=SerializeObject(myFinalData); 
			writer = t.CreateText();

		} 
		writer.Write(_data); 
		writer.Close(); 
		//Limpiando
		Admin_level0.datos.HistorialPartidasNivel1.Clear ();
		Admin_level0.datos.HistorialPartidasNivel2.Clear ();
		Admin_level0.datos.HistorialPartidasNivel3.Clear ();
		Debug.Log("File written and data cleared."); 
	} 

	public static IEnumerator IfNewUploadXMLToServer(){ 
		string filePath = GameSaveLoad._FileLocation +"\\"+GameSaveLoad._PacienteName+"_DataSandwich.xml";
		Debug.Log (filePath);
		// Create a Web Form
		WWWForm form = new WWWForm();
		if (File.Exists (filePath)) {
			StreamReader r = File.OpenText (filePath); 
			string _info = r.ReadToEnd (); 
			r.Close (); 
			Debug.Log ("File Read");
			byte[] levelData =Encoding.UTF8.GetBytes(_info);
			string fileName = Admin_level0.datos.id.ToString ()+"_"+"DataSandwich.xml";
			form.AddField("file","file");
			form.AddBinaryData ( "file", levelData, fileName,"text/xml");
			Debug.Log("sin error");
			Debug.Log (fileName);
			WWW w = new WWW("http://132.248.16.11/unity/uploadXML.php",form);
			//print("www created");
			yield return w;
			yield return new WaitForSeconds (1f);
			Debug.Log("enviado");
			Debug.Log("Me responde"+w.text.ToString());
		} else {
			Debug.Log("File Doesnt exist");
		}
	}


	//Deserializamos la info del xml y la guardamos en la cadena _data
	static void LoadXML() 
	{ 
		string filePath =( _FileLocation + "\\" + _PacienteName + "_DataSandwich").ToString();
		if (File.Exists (filePath)) {
			StreamReader r = File.OpenText (filePath); 
			string _info = r.ReadToEnd (); 
			r.Close (); 
			_data = _info; 
			Debug.Log ("File Read");
		} else {
			Debug.Log("File Doesnt exist");
			_data="";
		}
	} 

	//Sobrecarga del método anterior
	static void LoadXML(string filePath) 
	{ 
		if (File.Exists (filePath)) {
			StreamReader r = File.OpenText (filePath); 
			string _info = r.ReadToEnd (); 
			r.Close (); 
			_data = _info; 
			Debug.Log ("File Read");
		} else {
			Debug.Log("File Doesnt exist");
			_data="";
		}
	} 

}