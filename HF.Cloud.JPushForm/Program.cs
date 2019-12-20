using HF.Cloud.WcfJpush;
using HF.Cloud.WcfJpush.common;
using HF.Cloud.WcfJpush.common.resp;
using HF.Cloud.WcfJpush.push.mode;
using HF.Cloud.WcfJpush.push.notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace HF.Cloud.JPushForm
{
    class Program
    {
        public static String TITLE = "杨殿君极光测试，测试哒";
        public static String ALERT = "测试";
        public static String MSG_CONTENT = "哒哒哒大";
        public static String REGISTRATION_ID = "0900e8d85ef";
        public static String TAG = "tag_api";
        public static String app_key = "9279cc310c3eac26ffb9af79";
        public static String master_secret = "20bceaa5e61ca7939037f221";          

        static void Main(string[] args)
        {
            //Console.WriteLine("*****开始发送******");
            JPushClient client = new JPushClient(app_key, master_secret);
            PushPayload payload = PushObject_All_All_Alert(args);
            try
            {
                var result = client.SendPush(payload);

                ////由于统计数据并非非是即时的,所以等待一小段时间再执行下面的获取结果方法
                //System.Threading.Thread.Sleep(10000);
                ///*如需查询上次推送结果执行下面的代码*/
                //var apiResult = client.getReceivedApi(result.msg_id.ToString());
                //var apiResultv3 = client.getReceivedApi_v3(result.msg_id.ToString());
                ///*如需查询某个messageid的推送结果执行下面的代码*/
                //var queryResultWithV2 = client.getReceivedApi("1739302794");
                //var querResultWithV3 = client.getReceivedApi_v3("1739302794");
            }
            catch (APIRequestException e)
            {
                //Console.WriteLine("Error response from JPush server. Should review and fix it. ");
                //Console.WriteLine("HTTP Status: " + e.Status);
                //Console.WriteLine("Error Code: " + e.ErrorCode);
                //Console.WriteLine("Error Message: " + e.ErrorCode);
            }
            catch (APIConnectionException e)
            {
                //Console.WriteLine(e.Message);
            }
            //Console.WriteLine("*****结束发送******");
            //Console.Read();
            //HF.Cloud.JPushForm.Program.c
        }
        public static PushPayload PushObject_All_All_Alert(string[] args)
        {
            PushPayload pushPayload = new PushPayload();
            pushPayload.platform = Platform.all();
            pushPayload.audience = Audience.all();
            pushPayload.message = Message.content(args[0])
                  .AddExtras("Type", args[1])
                  .AddExtras("SheetID", args[2])
                  .AddExtras("TypeName", args[4])
                  .AddExtras("SheetTitle", args[5])
                  .AddExtras("SheetStateCN", args[6])
                  .AddExtras("IDS", args[3]);
            //pushPayload.message = Message.content("显示内容为推送")
            //                  .AddExtras("Type", 1)
            //                  .AddExtras("SheetID", 76)
            //                  .AddExtras("TypeName", "任务")
            //                  .AddExtras("SheetTitle", "wmt")
            //                  .AddExtras("SheetStateCN","已受理")
            //                  .AddExtras("IDS","10049,10015,10034,10026");
            return pushPayload;
        }
    }
}
