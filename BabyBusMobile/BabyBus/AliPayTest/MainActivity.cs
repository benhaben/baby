using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Com.Alipay.Sdk.App;
using Android.Support.V4.App;
using Java.Lang;
using AlipayApp.AlipayAPI;
using AliPayTest;

namespace AlipayApp
{
    [Activity(Label = "֧����Mono", MainLauncher = true, Icon = "@drawable/icon", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class MainActivity : FragmentActivity
    {
        //�̻�PID
        public const string PARTNER = "2088712736582414";
        //�̻��տ��˺�
        public const string SELLER = "it@mreliable.com";
        //�̻�˽Կ��pkcs8��ʽ
        public const string RSA_PRIVATE = "MIICdQIBADANBgkqhkiG9w0BAQEFAASCAl8wggJbAgEAAoGBANvUsiE8DQEoLEIS\n" +
                                          "XDJSy5mIvnr9YxWLoGTAYdydzHdEA4HSAoAC4f8tuf8erzQa1ZlxEkNqMb6r8TI5\n" +
                                          "QR72ScE2P0csatiwi5x5YZDqeXgFZovy8QuTn1OtD5EqPLOBuZZtCH+2Yz2mB45h\n" +
                                          "k5alBEGWFiVAYCan5wodttcQ6sNjAgMBAAECgYBG7vPni3P6ypa1Xy1Gw7aUvS4R\n" +
                                          "i4+cVSiVOgqZ4IUoetbS3gwWeFeqOnwI2ULZgksoLvcgr7SLfPngJd9geUJEr/4M\n" +
                                          "FIG/r81MhhQLXHjywHoDtgl8xOzE6up0ZTNCYvLFZVmHS0YSKjpEouXSFsaJDd5s\n" +
                                          "RkGQgZqntwE4ENTdcQJBAO7Poqc50rMuUcXbzihsbU4twX/muRez3yeYJqfcRbT7\n" +
                                          "QMAxUcL2Oks4BiKRpXxI9wVgtYWEK2wZxGKe5gYDIIsCQQDrp1QCq2XfYPGcTxYA\n" +
                                          "vih9nQxaNYSPfPrpM3ysDH4j16TCriIdJl7uVol6mT1bhQwq237BzD8uWbUI+42C\n" +
                                          "NSuJAkB419LbwhPPndG9SHPy2qMZG2g+G3dv+hIjDAgLixgu87EZUBuqh0SKSYg5\n" +
                                          "N/BAiv+M1hokvPPoGMXajcOiKTTvAkAjpeplhPwiMI4cMTKI5jtF1U4bD2GAO03R\n" +
                                          "nUJM3I7waRy5fpIWisltkJW3gBryD0xp505jjrw4DMYAF92uRtDRAkBkvMzsNPHJ\n" +
                                          "MJqu18JKsNO9BRDlk7NcpXseTwoau3beLnOWpodoiaSCPrvDgpLskLoNgvnH61nS\n" +
                                          "7amVpsEtNSb3";
        //֧������Կ
        public const string RSA_PUBLIC = "";

        private const int SDK_PAY_FLAG = 1;
        //֧��
        private const int SDK_CHECK_FLAG = 2;
        //����˻�
        private Handler mHandler { get; set; }
        //Handler�����������ͺͽ�����Ϣ

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            RequestWindowFeature(WindowFeatures.NoTitle);
            SetContentView(Resource.Layout.pay_main);

            //֧��
            var pay = FindViewById<Button>(Resource.Id.pay);
            pay.Click += ConfirmAliPayBtn;

            //����˻�
            var check = FindViewById<Button>(Resource.Id.check);
            check.Click += ConfirmcheckAliPayBtn;

            #region �����̻߳ظ�Ȼ���ж��Ƿ�ɹ�
            mHandler = new Handler((msg) =>
                {
                    switch (msg.What)
                    {
                        case SDK_PAY_FLAG:
                            {
                                PayResult payResult = new PayResult((string)msg.Obj);
                                // ֧�������ش˴�֧���������ǩ�������֧����ǩ����Ϣ��ǩԼʱ֧�����ṩ�Ĺ�Կ����ǩ
                                string resultInfo = payResult.getResult();
                                string resultStatus = payResult.getResultStatus();

                                // �ж�resultStatus Ϊ��9000�������֧���ɹ�������״̬�������ɲο��ӿ��ĵ�
                                if (Android.Text.TextUtils.Equals(resultStatus, "9000"))
                                {
                                    Toast.MakeText(this, "֧���ɹ�", ToastLength.Short).Show();
                                }
                                else
                                {
                                    // �ж�resultStatus Ϊ�ǡ�9000����������֧��ʧ��
                                    // ��8000������֧�������Ϊ֧������ԭ�����ϵͳԭ���ڵȴ�֧�����ȷ�ϣ����ս����Ƿ�ɹ��Է�����첽֪ͨΪ׼��С����״̬��
                                    if (Android.Text.TextUtils.Equals(resultStatus, "8000"))
                                    {
                                        Toast.MakeText(this, "֧�����ȷ����", ToastLength.Short).Show();
                                    }
                                    else
                                    {
                                        // ����ֵ�Ϳ����ж�Ϊ֧��ʧ�ܣ������û�����ȡ��֧��������ϵͳ���صĴ���
                                        Toast.MakeText(this, "֧��ʧ��", ToastLength.Short).Show();
                                    }
                                }
                                break;
                            }
                        case SDK_CHECK_FLAG:
                            {
                                Toast.MakeText(this, "�����Ϊ��" + msg.Obj, ToastLength.Short).Show();
                                break;
                            }
                        default:
                            break;
                    }
                }
            );
            #endregion
        }

        //֧��
        void ConfirmAliPayBtn(object sender, EventArgs e)
        {
            // ����
            string orderInfo = getOrderInfo("���Ե���Ʒ", "�ò�����Ʒ����ϸ����", "0.01");
            // �Զ�����RSA ǩ��
            string sign = signRsa(orderInfo);
            try
            {
                // �����sign ��URL����
                sign = Java.Net.URLEncoder.Encode(sign, "UTF-8");
            }
            catch (Java.Lang.Exception eex)
            {
                eex.StackTrace.ToString();
            }
            // �����ķ���֧���������淶�Ķ�����Ϣ
            string payInfo = orderInfo + "&sign=\"" + sign + "\"&" + getSignType();
            var payRunnable = new Runnable(() =>
                {
                    var alipay = new PayTask(this);
                    var result = alipay.Pay(payInfo);
                    var msg = new Message();
                    msg.What = SDK_PAY_FLAG;
                    msg.Obj = result;
                    mHandler.SendMessage(msg);
                });
            var payThread = new Thread(payRunnable);
            payThread.Start();
        }

        //����˻�
        void ConfirmcheckAliPayBtn(object sender, EventArgs e)
        {
            var checkAlipayRunnable = new Runnable(() =>
                {
                    var payTask = new PayTask(this);
                    var isExist = payTask.CheckAccountIfExist();

                    var msg = new Message();
                    msg.What = SDK_CHECK_FLAG;
                    msg.Obj = isExist;
                    mHandler.SendMessage(msg);
                });
            var payThread = new Thread(checkAlipayRunnable);
            payThread.Start();
        }

        //��ȡSDK�汾
        public void getSDKVersion()
        {
            PayTask payTask = new PayTask(this);
            string version = payTask.Version;
            Toast.MakeText(this, version, ToastLength.Short).Show();
        }

        //����������Ϣ
        public string getOrderInfo(string subject, string body, string price)
        {
            // ǩԼ���������ID
            string orderInfo = "partner=" + "\"" + PARTNER + "\"";
            // ǩԼ����֧�����˺�
            orderInfo += "&seller_id=" + "\"" + SELLER + "\"";
            // �̻���վΨһ������
            orderInfo += "&out_trade_no=" + "\"" + getOutTradeNo() + "\"";
            // ��Ʒ����
            orderInfo += "&subject=" + "\"" + subject + "\"";
            // ��Ʒ����
            orderInfo += "&body=" + "\"" + body + "\"";
            // ��Ʒ���
            orderInfo += "&total_fee=" + "\"" + price + "\"";
            // �������첽֪ͨҳ��·��
            orderInfo += "&notify_url=" + "\"" + "http://notify.msp.hk/notify.htm" + "\"";
            // ����ӿ����ƣ� �̶�ֵ
            orderInfo += "&service=\"mobile.securitypay.pay\"";
            // ֧�����ͣ� �̶�ֵ
            orderInfo += "&payment_type=\"1\"";
            // �������룬 �̶�ֵ
            orderInfo += "&_input_charset=\"utf-8\"";
            // ����δ����׵ĳ�ʱʱ��
            // Ĭ��30���ӣ�һ����ʱ���ñʽ��׾ͻ��Զ����رա�
            // ȡֵ��Χ��1m��15d��
            // m-���ӣ�h-Сʱ��d-�죬1c-���죨���۽��׺�ʱ����������0��رգ���
            // �ò�����ֵ������С���㣬��1.5h����ת��Ϊ90m��
            orderInfo += "&it_b_pay=\"30m\"";
            // extern_tokenΪ���������Ȩ��ȡ����alipay_open_id,���ϴ˲����û���ʹ����Ȩ���˻�����֧��
            // orderInfo += "&extern_token=" + "\"" + extern_token + "\"";
            // ֧��������������󣬵�ǰҳ����ת���̻�ָ��ҳ���·�����ɿ�
            orderInfo += "&return_url=\"m.alipay.com\"";
            // �������п�֧���������ô˲���������ǩ���� �̶�ֵ ����ҪǩԼ���������п����֧�������ܹ�ã��            // orderInfo += "&paymethod=\"expressGateway\"";
            return orderInfo;
        }

        //�����̻������ţ���ֵ���̻���Ӧ����Ψһ�����Զ����ʽ�淶��
        public string getOutTradeNo()
        {
            Java.Text.SimpleDateFormat format = new Java.Text.SimpleDateFormat("MMddHHmmss", Java.Util.Locale.Default);
            Java.Util.Date date = new Java.Util.Date();
            string key = format.Format(date);
            Java.Util.Random r = new Java.Util.Random();
            key = key + r.NextInt();
            key = key.Substring(0, 15);
            return key;
        }

        //�Զ�����Ϣ����ǩ��   ��ǩ��������Ϣ
        public string signRsa(string content)
        {
            return SignUtils.sign(content, RSA_PRIVATE);
        }

        //��ȡǩ����ʽ
        public string getSignType()
        {
            return "sign_type=\"RSA\"";
        }
    }
}