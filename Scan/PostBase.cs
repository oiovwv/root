// OSR_SP_Traceability.PostBase
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Scan.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Windows.Forms;

public class PostBase
{
	#region
	//20220523

	private static string[] GetParams(string paramValue)
	{
		var values = !string.IsNullOrEmpty(paramValue) ? paramValue.Split(',') : new string[] { };
		return values;
	}
	public static string GetPCode(string isPro, string pcode)
	{

		string url = isPro == "Y" ? "https://API.osram.com:35443/REST/87ID2380PL/SPTracePara_PcodeSet" : "https://API-TEST.osram.com:35443/REST/87ID2380PL/SPTracePara_PcodeSet";

		string[] plants = GetParams(pcode);
		string result = string.Empty;
		string strWhere = FormatParams("Pcode", plants);
		Dictionary<string, string> param = GetUrlParams(strWhere, "Pcode");
		StringBuilder bulider = GetBuilder(url, param);
		byte[] bytes = new byte[] { };
		try
		{
			result = GetResponseResult(bulider.ToString(), "GET", bytes, isPro);
		}
		catch (Exception ex)
		{
			result = ex.Message;
		}
		return result;
	}


	public static string GetObSetData(string isPro, string delivery)
	{
		string url = isPro == "Y" ? "https://API.osram.com:35443/REST/87ID2380PL/SPTracePara_OBSet" : "https://API-TEST.osram.com:35443/REST/87ID2380PL/SPTracePara_OBSet";
		string[] deliverys = GetParams(delivery);
		string result = string.Empty;
		string strWhere = FormatParams("ObSet", deliverys);
		Dictionary<string, string> param = GetUrlParams(strWhere, "ObSet");
		StringBuilder bulider = GetBuilder(url, param);
		byte[] bytes = new byte[] { };
		try
		{
			result = GetResponseResult(bulider.ToString(), "GET", bytes, isPro);
		}
		catch (Exception ex)
		{
			result = ex.Message;
		}
		return result;
	}


	public static string GetTraceabilitySave(string isPro, string postJson)
	{

		string url = isPro == "Y" ? "https://API.osram.com:35443/REST/87ID2380PL/SPTracePara_SaveSet" : "https://API-TEST.osram.com:35443/REST/87ID2380PL/SPTracePara_SaveSet";
		string result = string.Empty;
		var tModel = new Traceability();
		byte[] bytes = FormatPostParam(postJson, tModel);
		try
		{
			result = GetResponseResult(url, "POST", bytes, isPro);
		}
		catch (Exception ex)
		{
			result = ex.Message;
		}
		return result;
	}


	public static string GetOEMObSetData(string isPro, string delivery)
	{

		string url = isPro == "Y" ? "https://API.osram.com:35443/REST/87ID2380PL/NonOEMPara_OBSet" : "https://API-TEST.osram.com:35443/REST/87ID2380PL/NonOEMPara_OBSet";
		string[] deliverys = GetParams(delivery);
		string result = string.Empty;
		string strWhere = FormatParams("ObSet", deliverys);
		Dictionary<string, string> param = GetUrlParams(strWhere, "OEM");
		StringBuilder bulider = GetBuilder(url, param);
		byte[] bytes = new byte[] { };
		try
		{
			result = GetResponseResult(bulider.ToString(), "GET", bytes, isPro);
		}
		catch (Exception ex)
		{
			result = ex.Message;
		}
		return result;
	}


	public static string GetOEMTraceabilitySave(string isPro, string postJson)
	{
		string url = isPro == "Y" ? "https://API.osram.com:35443/REST/87ID2380PL/NonOEMPara_SaveSet" : "https://API-TEST.osram.com:35443/REST/87ID2380PL/NonOEMPara_SaveSet";
		string result = string.Empty;
		var tModel = new OEMTraceability();
		byte[] bytes = FormatPostParam(postJson, tModel);
		try
		{
			result = GetResponseResult(url, "POST", bytes, isPro);			
		}
		catch (Exception ex)
		{
			result = ex.Message;			
		}
		return result;
	}
	private static StringBuilder GetBuilder(string url, Dictionary<string, string> param)
	{
		StringBuilder bulider = new StringBuilder();
		bulider.Append(url);
		if (param.Count > 0)
		{
			bulider.Append("?");
			int i = 0;
			foreach (var item in param)
			{
				if (i > 0)
					bulider.Append("&");
				bulider.AppendFormat("{0}={1}", item.Key, item.Value);
				i++;
			}
		}
		return bulider;
	}
	private static string FormatParams(string type, string[] param)
	{
		string strWhere = string.Empty;
		if (param.Count() > 1)
		{
			if (type == "Plant")
			{
				strWhere = "(Plant ge '" + param[0] + "' and Plant le '" + param[1] + "')";
			}
			else if (type == "CreatedOn")
			{
				strWhere = " and (CreatedOn ge '" + param[0] + "' and CreatedOn le '" + param[1] + "')";
			}
			else if (type == "Delivery")
			{

				strWhere = " and (Delivery ge '" + param[0] + "' and Delivery le '" + param[1] + "')";
			}
		}
		else if (param.Count() == 1)
		{
			if (type == "Plant")
			{
				strWhere = "(Plant eq '" + param[0] + "')";
			}
			else if (type == "CreatedOn")
			{
				strWhere = " and (CreatedOn eq '" + param[0] + "')";
			}
			else if (type == "Delivery")
			{
				strWhere = " and (Delivery eq '" + param[0] + "')";
				if (param[0].IndexOf("26") == 0)
				{
					strWhere += " and (Mode eq 'I')";
				}
			}
			else if (type == "Mode")
			{
				strWhere = " and (Mode eq '" + param[0] + "')";
			}
			else if (type == "Ean")
			{
				strWhere = " and (EAN eq '" + param[0] + "')";
			}
			else if (type == "Pcode")
			{
				strWhere = "(Pcode eq '" + param[0] + "')";
			}
			else if (type == "ObSet")
			{
				strWhere = "(Delivery eq '" + param[0] + "')";
			}
		}

		return strWhere;
	}
	private static Dictionary<string, string> GetUrlParams(string strWhere, string type)
	{
		Dictionary<string, string> param = new Dictionary<string, string>();
		param.Add("$filter", strWhere);
		if (type == "Customer")
		{
			param.Add("$expand", "ExCust,ExDeliveryHeader,ExDeliveryItem,ExVendor,ExReturn");
		}
		else if (type == "Order")
		{
			param.Add("$expand", "ExDeliveryHeader,ExDeliveryItem,ExVendor,ExText");
		}
		else if (type == "Mat")
		{
			param.Add("$expand", "MatDocData");
		}
		else if (type == "Product")
		{
			param.Add("$expand", "MaterialData");
		}
		else if (type == "Pcode")
		{
			param.Add("$expand", "ExPcodeMsg");
		}
		else if (type == "ObSet")
		{
			param.Add("$expand", "ExOBQty,ExOBMsg");
		}
		else if (type == "OEM")
		{
			param.Add("$expand", "ExOBQty_NonOEM,ExOBMsg_NonOEM");
		}
		param.Add("$format", "json");
		return param;
	}
	public static string GetResponseResult(string url, string method, byte[] bytes, string env)
	{
		string result = string.Empty;
		string key = string.Empty;
		if (env == "N")
		{
			key = "API-Key ZGM1MjllYmItOTQ3MC00MjlhLTkyZmUtN2ZkMzdkYjY2YWQw";
		}
		else
		{
			key = "API-Key MTNhYjRhOTUtMTJlMC00Y2MxLTkyNzktZGNhMTRmNDI3N2Q0";
		}
		try
		{
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			request.Method = method;
			request.Accept = "application/json";
			request.ContentType = "application/json";
			request.Headers.Add("Authorization", key);

			if (bytes.Length > 0)
			{
				request.ContentLength = bytes.Length;
				Stream strStream = request.GetRequestStream();
				strStream.Write(bytes, 0, bytes.Length);
				strStream.Close();
			}

			System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12; //加上这一句 //添加参数
			ServicePointManager.ServerCertificateValidationCallback += RemoteCertificateValidate;
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			Stream stream = response.GetResponseStream();
			//获取内容
			using (StreamReader reader = new StreamReader(stream))
			{
				result = reader.ReadToEnd();
				reader.Close();
				stream.Close();
			}
		}
		catch (Exception ex)
		{
			throw new Exception();
		}
		return result;
	}
	private static byte[] FormatPostParam<T>(string postJson, T model)
	{
		model = JsonConvert.DeserializeObject<T>(postJson);
		var param = JsonConvert.SerializeObject(model);
		byte[] bytes = Encoding.UTF8.GetBytes(param);
		return bytes;
	}
	private static bool RemoteCertificateValidate(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors error)
	{
		//为了通过证书验证，总是返回true
		return true;
	}

	public static string GetApiResultNew(string param, int type, string isPro)
	{
		string result = string.Empty;
		switch (type)
		{
			case 1:
				result = GetPCode(isPro, param);
				break;
			case 2:
				result = GetObSetData(isPro, param);
				break;
			case 3:
				result = GetTraceabilitySave(isPro, param);
				break;
			case 4:
				result = GetOEMObSetData(isPro, param);
				break;
			case 5:
				result = GetOEMTraceabilitySave(isPro, param);
				break;
		}
		return result;
	}


	#endregion



	public static string GetApiResult(string param, string key, int type, string isPro)
	{
		string result = string.Empty;
		try
		{
			NameValueCollection VarPost = new NameValueCollection();
			VarPost.Clear();
			VarPost.Add("Pro", isPro);
			if (key == "P")
			{
				VarPost.Add("Pcode", param);
			}
			else
			{
				VarPost.Add("Delivery", param);
			}
			return PostData(GetApiFnByType(type), VarPost);
		}
		catch (Exception)
		{
			return string.Empty;
		}
	}


	public static string GetApiResult(string modelJson, int type, string isPro)
	{
		string result = string.Empty;
		try
		{
			NameValueCollection VarPost = new NameValueCollection();
			VarPost.Clear();
			VarPost.Add("Pro", isPro);
			VarPost.Add("Body", modelJson);
			return PostData(GetApiFnByType(type), VarPost);
		}
		catch (Exception)
		{
			return "请求出错";
		}
	}

	private static string PostData(string actionName, NameValueCollection VarPost)
	{
		try
		{
			WebClient web = new WebClient();
			byte[] byRemoteInfo = web.UploadValues("http://10.205.200.28:9527/api/OSR/" + actionName, "POST", VarPost);
            //byte[] byRemoteInfo = web.UploadValues("http://localhost:11379/api/OSR/" + actionName, "POST", VarPost);
            return Encoding.UTF8.GetString(byRemoteInfo);
		}
		catch (Exception ex)
		{
			throw new Exception(ex.Message);
		}
	}

	private static string GetApiFnByType(int type)
	{
		string api = string.Empty;
		switch (type)
		{
		case 1:
			api = "GetPCode";
			break;
		case 2:
			api = "GetObSetData";
			break;
		case 3:
			api = "GetTraceabilitySave";
			break;
		case 4:
			api = "GetOEMObSetData";
			break;
		case 5:
			api = "GetOEMTraceabilitySave";
			break;
		}
		return api;
	}

	public static T ToObject<T>(string content)
	{
		return JsonConvert.DeserializeObject<T>(content);
	}

	public static string ModelToJson<T>(T model)
	{
		JsonSerializer serializer = new JsonSerializer();
		StringWriter sw = new StringWriter();
		serializer.Serialize(new JsonTextWriter(sw), model);
		return sw.GetStringBuilder().ToString();
	}

	public static bool ConfirmClose(string tips)
	{
		DialogResult result = MessageBox.Show(tips, "操作提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
		return result == DialogResult.OK;
	}

	public static T GetNeedScanInfo<T>(string clientOrderNo, int type, T model, string env)
	{
		string result = string.Empty;
		if (!string.IsNullOrEmpty(clientOrderNo))
		{
			//result = GetApiResult(clientOrderNo, "O", type, env);
			result = GetApiResultNew(clientOrderNo, type, env);
			if (!string.IsNullOrEmpty(result) && result.IndexOf("异常") < 0)
			{
				model = ToObject<T>(result);
			}
			else
			{
				MessageBox.Show("请求失败；" + result);
			}
		}
		else
		{
			MessageBox.Show("请输入订单号~");
		}
		return model;
	}


	public static void MessageTips(string msg)
	{
		SoundPlayer sndPlayer = new SoundPlayer(Application.StartupPath + "/error.wav");
		sndPlayer.PlayLooping();
		MessageBox.Show(msg);
		sndPlayer.Stop();
	}

	public static void playSuccessTips()
	{
		SoundPlayer sndPlayer = new SoundPlayer(Application.StartupPath + "/success.wav");
		sndPlayer.PlayLooping();
		Thread.Sleep(1000);
		sndPlayer.Stop();
	}
}
