// OSR_SP_Traceability.PostBase
using Newtonsoft.Json;
using System;
using System.Collections.Specialized;
using System.IO;
using System.Media;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

public class PostBase
{
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
		if (!string.IsNullOrEmpty(clientOrderNo))
		{
			string result = GetApiResult(clientOrderNo, "O", type, env);
			if (!string.IsNullOrEmpty(result))
			{
				model = ToObject<T>(result);
			}
			else
			{
				MessageBox.Show("请求失败，暂无数据~");
			}
		}
		else
		{
			MessageBox.Show("请输入订单号~");
		}
		return model;
	}

	public static void playSuccess()
	{
		SoundPlayer sndPlayer = new SoundPlayer(Application.StartupPath + "/success.wav");
		sndPlayer.PlayLooping();
	}

	public static void stop()
	{
		SoundPlayer sndPlayer = new SoundPlayer(Application.StartupPath + "/success.wav");
		sndPlayer.Stop();
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
