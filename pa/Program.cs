using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace pa
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("半次元");

            //存正则提取出来的链接
            List<string> zhuye = new List<string>();
            zhuye.Clear();//毫无卵用

            //存下一页的链接
            List<string> xiayiye = new List<string>();
            xiayiye.Clear();//毫无卵用

            //请求网页链接的类
            WebClient lianjie1 = new WebClient();

            //指定下载网页的编码，防止出现乱码（网页是哪个编码就哪个编码）
            lianjie1.Encoding = System.Text.Encoding.UTF8;

            //经过查看源码得知网页翻页只有单个字符改变，（页数）代码如下，只有数字改变
            //<a href="/acg/cos/index_18.html">18</a>
            int a = 0;
            //所以，我们将1-末尾页20页链接拼接好存入集合中
            for (int i = 2; i <= 20; i++)
            {
                //起始页与其他页面不同
                if (a == 0)
                {
                    xiayiye.Add(@"https://t2cy.com/acg/cos/index.html");
                }
                a++;
                xiayiye.Add(@"https://t2cy.com/acg/cos/index_" + i + ".html");
            }

            for (int i = 0; i < xiayiye.Count; i++)//循环输出一下看看页面是否正确
            {
                Console.WriteLine(xiayiye[i]);
            }
            Console.WriteLine("当前网页共有{0}个页面", xiayiye.Count);
            Console.WriteLine("按下回车继续！");
            Console.ReadKey();

            //存好以后将下一页的链接循环一遍，保存主页cos链接
            for (int i = 0; i < xiayiye.Count; i++)
            {
                string sw = lianjie1.DownloadString(xiayiye[i]);
                //<a href=""(.+?)"".+?block;"></a>
                MatchCollection tiqu = Regex.Matches(sw, @"<a href=""(.+?)"".+?");
                foreach (Match tiqu1 in tiqu)
                {
                    //将杂项链接去除
                    if (tiqu1.Groups[1].Value.Length < 36)
                    {

                    }
                    else
                    {
                        if (zhuye.Contains(@"https://t2cy.com" + tiqu1.Groups[1].Value))
                        {

                        }
                        else
                        {
                            zhuye.Add(@"https://t2cy.com" + tiqu1.Groups[1].Value);
                        }

                    }
                }

            }
            //输出查看提取的主页链接
            for (int i = 0; i < zhuye.Count; i++)
            {
                Console.WriteLine(zhuye[i]);
            }
            Console.WriteLine(zhuye.Count());
            Console.WriteLine("按下回车继续");
            Console.ReadKey();

            //存好了合集后我们将合集里面的图片一个个下载下来
            for (int i = 0; i < zhuye.Count; i++)
            {
                List<string> tp = new List<string>();
                tp.Clear();
                List<string> mz = new List<string>();
                mz.Clear();
                string swsa = lianjie1.DownloadString(zhuye[i]);//下载页面
                //^<p><img src=""(.+?)"" alt="".+?cos""/></p>$
                MatchCollection jihe34 = Regex.Matches(swsa, @".+?<img src=""(.+?)"" alt="".+?cos"".+?");//提取页面里图片链接
                //int n = 0;
                //循环集合提取图片li链接
                foreach (Match abcd in jihe34)
                {
                    //n++;
                    if (true)
                    {
                        tp.Add(@"https://t2cy.com" + abcd.Groups[1].Value);
                    }
                    else
                    {

                    }
                }
                //提取另一种图片链接
                MatchCollection jihe35 = Regex.Matches(swsa, @".+?data-loadsrc=""(.+?)"" alt=""([^【】]+?)"".+?");
                foreach (Match it in jihe35)
                {
                    string s = it.Groups[2].Value;
                    if (it.Groups[1].Value.Length > 75)
                    {

                    }
                    else
                    {
                        if (it.Groups[2].Value.Length == s.Length)
                        {
                            tp.Add(@"https://t2cy.com" + it.Groups[1].Value);
                            mz.Add(it.Groups[2].Value);//保存图片名字
                        }
                    }
                }
                if (i > 0)
                {
                    Console.WriteLine("为了防止服务器请求频繁，下载一个合集就等几秒钟吧");
                    Console.WriteLine("按下回车继续");
                    Console.ReadKey();
                }
                string swwaw = @"D:\图片下载\" + mz[1].ToString();//创建一个文件夹，将循环里保存的名字作为文件夹名字
                Directory.CreateDirectory(swwaw);
                int jiusan = 0;
                //图片链接和文件名字都保存好了，那么开始循环下载
                for (int j = 0; j < tp.Count; j++)
                {

                    try
                    {
                        Console.WriteLine(tp[j]);//正在下载的链接
                        Console.WriteLine("开始下载{0}", tp[i]);
                        lianjie1.DownloadFile(tp[j], @swwaw + "\\" + mz[1] + j + ".jpg");//将图片下载到指定的文件夹内，并添加后缀名
                        Console.WriteLine("下载中.........");
                        Console.WriteLine("下载完成");
                        Console.WriteLine();
                    }
                    catch
                    {

                    }
                }

            }
            Console.WriteLine("程序运行结束");
            Console.ReadKey();
        }
    }
}
