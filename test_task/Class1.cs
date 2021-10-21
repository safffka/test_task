using System;
using System.Data;
using System.IO;
using System.Xml;
using  System.Data.SQLite;
using System.Text;
using System.Data.Common;

public class Changes
{
	public string Assembly_num { get; set; }
	public int Change_num { get; set; }
	public string Author { get; set; }
	public string Reasons { get; set; }
	public string Change_date { get; set; }

	
}
public class Changes_Processing
{
	
	public void UploadChanges(Db db)
	{

		XmlDocument xml = new XmlDocument();
		xml.Load("XMLFile1.xml");
		var Change_1 = new Changes();
		var i = 1;
		Change_1.Assembly_num = xml.SelectSingleNode("root/Version").InnerText;
		foreach (XmlNode n in xml.SelectNodes("root/Changes/Change"))
		{	
			Change_1.Change_num = i;
			Change_1.Author = n.SelectSingleNode("Author").InnerText;
			Change_1.Reasons = n.SelectSingleNode("Changings").InnerText;
			Change_1.Change_date =n.SelectSingleNode("Change_date").InnerText;
			i++;
			var sql = "('" + Convert.ToString(Change_1.Assembly_num) + "', " + Convert.ToString(Change_1.Change_num) + ", '" + Change_1.Author + "', '" + Change_1.Reasons + "', '" + Change_1.Change_date + "')";
			db.insert_db(sql);
		}
		
	}
	
}