using System;
using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;

namespace MoodDetectorMobile
{
	public class FacebookPostViewHolder: Java.Lang.Object
	{
		public TextView txtPostId { get; set; }
		public TextView txtMessage { get; set; }
		public TextView txtCreatedDate { get; set; }
		public TextView txtKeyPhrases { get; set; }
	}
	public class FacebookPost
	{
		public string PostId { get; set; }
		public string Message { get; set; }
		public DateTimeOffset CreatedDate { get; set; }
		public string KeyPhrases { get; set; }
	}

	public class FacebookPostsAdapter : BaseAdapter<FacebookPost>
	{
		private List<FacebookPost> _data = null;
		private Activity _context;
		public FacebookPostsAdapter(Activity pContext, List<FacebookPost> pPosts)
		{
			this._context = pContext;
			this._data = pPosts;
		}

		public override FacebookPost this[int position]
		{
			get
			{
				return this._data[position];
			}
		}

		public override int Count
		{
			get
			{
				return this._data.Count;
			}
		}

		public override long GetItemId(int position)
		{
			//we can't use PostId because its format is a string, and cannot be converted to a long
			return position;
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			var view = convertView;
			if (view == null)
			{
				view = 
					this._context.LayoutInflater.Inflate(Resource.Layout.MyFacebookPostsRow, parent, false);
				var postId = view.FindViewById<TextView>(Resource.Id.txtPostId);
				var message = view.FindViewById<TextView>(Resource.Id.txtMessage);
				var createdDate = view.FindViewById<TextView>(Resource.Id.txtCreatedDate);
				var keyPhrases = view.FindViewById<TextView>(Resource.Id.txtKeyPhrases);

				view.Tag = new FacebookPostViewHolder()
				{
					txtCreatedDate = createdDate,
					txtMessage = message,
					txtPostId = postId,
					txtKeyPhrases = keyPhrases
				};
			}
			var holder = (FacebookPostViewHolder)view.Tag;
			holder.txtPostId.Text = this._data[position].PostId;
			holder.txtMessage.Text = this._data[position].Message;
			holder.txtCreatedDate.Text = this._data[position].CreatedDate.ToString("yyyyMMdd");
			holder.txtKeyPhrases.Text = this._data[position].KeyPhrases;
			return view;

		}
	}
}
