1����֧�����˺��������ù�Կ��https://uemprod.alipay.com/user/ihome.htm��
�ҵ�֧����-��ǩԼ����-���鿴PID|Key(����֧����֧������)��>���߲�Ʒ��Կ����wapר�ã�->����<RSA����>

2������MainActivity.cs������ĸ�����
//�̻�PID	public const string PARTNER = ""; ����˵һ�¾���֧������֧���������ģ���ҵ�û�������֧����֮��֧�����ͻ��ṩһ��������id��������ν��pid����2088��ͷ��16λ�����֣�
//�̻��տ��˺�	public const string SELLER = "";   ��������������տ��õ�֧�����˺ţ�Ҫ������ʱ��ͬһ��
//�̻�˽Կ��pkcs8��ʽ	public const string RSA_PRIVATE = "";
//֧������Կ	public const string RSA_PUBLIC = "";

PS���̻�˽Կ��pkcs8��ʽ��֧������Կ�����ķ�����ʹ��openssl.zip��
<1>���ҵ��ļ�openssl.zip����ѹ����Ŀ¼\openssl\bin�ļ��������openssl.exe���ɣ��������֧�����ṩ�ġ�
<2>��RSA˽Կ��genrsa -out rsa_private_key.pem 1024   �����������п��Կ���bin�ļ������������˽Կ
<3>��RSA��Կ��rsa -in rsa_private_key.pem -pubout -out rsa_public_key.pem    �����������к���Կ��������˹�Կ
<4>��PKCS8�����˽Կ��pkcs8 -topk8 -inform PEM -in rsa_private_key.pem -outform PEM -nocrypt    ������󣬽����ɵĶ������������ŵ��ı��ļ�������С������Ҫ�õ��ģ�������begin��end������һ�������