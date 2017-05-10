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

namespace BabyBus.Droid.Views.Content
{
	[Activity(Label = "�׽̾���", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
	public class TeachingExperienceView : ActivityBase
	{
		private const string TitleResourceKey = "Title";
		private const string LinkResourceKey = "Link";
		private readonly string[] titles = {
			"��������ҩ���������㶼��������",
			"��������˯���߱���8��ԭ��",
			"�����ӳԳ��������۾�",
			"�����Ƣ���ǳɾͺ��ӵĽ����������",
			"ĸ�׵��Ը����Ժ���Ϊ��Ӱ�캢�ӵ�һ��",
			"��ΰѰ��͹��ͬʱ������",
		};

		private readonly string[] links = {
            "TeachingExperence/to healthy baby medicine.htm",
            "TeachingExperence/Baby sleep at night to kick a quilt for 8 Reasons.htm",
            "TeachingExperence/Give children eat a bright eyes.htm",
            "TeachingExperence/Mother's temper is the biggest factor in the future success of children.htm",
            "TeachingExperence/The mother character language and behavior will affect the child's life.htm",
            "TeachingExperence/How to make love and rules also give children.htm"
        };
        protected override void OnCreate(Bundle bundle) {
            base.OnCreate(bundle);

            // Create your application here
            SetContentView(Resource.Layout.Page_Content_Index);

            // Create Simple Adapter
            var listview = FindViewById<ListView>(Resource.Id.content_list);
            var list = new List<IDictionary<string, object>>();
            for (int i = 0; i < titles.Length; i++)
            {
                var dic = new JavaDictionary<string, object>();
                dic.Add(TitleResourceKey,titles[i]);
                dic.Add(LinkResourceKey,links[i]);
                list.Add(dic);
            }
            var adapter = new SimpleAdapter(this,
                list,
                Resource.Layout.Item_Content,
                new[] {TitleResourceKey},
                new[] {Resource.Id.item_content_title});
            listview.Adapter = adapter;

            listview.ItemClick += (sender, args) => {
                var link = links[args.Position];
                var intent = new Intent(this, typeof (ContentDetailView));
                intent.PutExtra("FileName", link);
                intent.PutExtra("Title", "�׽̾���");
                StartActivity(intent);
            };
        }
    }
}