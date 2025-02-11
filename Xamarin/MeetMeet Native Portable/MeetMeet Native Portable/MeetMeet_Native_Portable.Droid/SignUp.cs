﻿
using System;

using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;


namespace MeetMeet_Native_Portable.Droid
{
    /// <summary>
    /// Class to change layout when SignUp button pressed
    /// Handles Sign Up event arguments. Allows for username and password inputed in 
    /// dialog_sign_up layout to be retrieved.
    /// </summary>
    public class OnSignUpEventArgs : EventArgs
	{

		private string mUserName;
		private string mPassword;

		/// <summary>
		/// Gets or sets the name of the user.
		/// </summary>
		/// <value>The name of the user.</value>
		public string UserName {
			get { return mUserName; }
			set { mUserName = value; }
		}

		/// <summary>
		/// Gets or sets the password.
		/// </summary>
		/// <value>The password.</value>
		public string Password {
			get { return mPassword; }
			set { mPassword = value; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MeetMeet_Native_Portable.Droid.OnSignUpEventArgs"/> class.
		/// </summary>
		/// <param name="firstName">First name.</param>
		/// <param name="email">Email.</param>
		/// <param name="password">Password.</param>
		public OnSignUpEventArgs (String userName, String email, string password) : base ()
		{
			UserName = userName;
			Password = password;
		}
	}

	/// <summary>
	/// Provides functionality to Sign Up dialog fragment.
	/// </summary>
	[Activity (Label = "SignUp")]			
	class SignUp : DialogFragment
	{
		private EditText mTxtUserName;
		private EditText mTxtPassConfirm;
		private EditText mTxtPassword;
		private Button mBtnSignUp;

		/// <summary>
		/// The m on sign up complete.
		/// </summary>
		public EventHandler<OnSignUpEventArgs> mOnSignUpComplete;

		/// <param name="inflater">The LayoutInflater object that can be used to inflate
		///  any views in the fragment,</param>
		/// <param name="container">If non-null, this is the parent view that the fragment's
		///  UI should be attached to. The fragment should not add the view itself,
		///  but this can be used to generate the LayoutParams of the view.</param>
		/// <param name="savedInstanceState">If non-null, this fragment is being re-constructed
		///  from a previous saved state as given here.</param>
		/// <summary>
		/// Called to have the fragment instantiate its user interface view.
		/// </summary>
		/// <returns>To be added.</returns>
		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			base.OnCreateView (inflater, container, savedInstanceState);

			var view = inflater.Inflate (Resource.Layout.dialog_sign_up, container, false);

			// Setting Button and edittext References from dialog_sign_up layout
			mTxtUserName = view.FindViewById<EditText> (Resource.Id.txtUserName);
			mTxtPassConfirm = view.FindViewById<EditText> (Resource.Id.txtPassConfirm);
			mTxtPassword = view.FindViewById<EditText> (Resource.Id.txtPassword);
			mBtnSignUp = view.FindViewById<Button> (Resource.Id.btnDialogEmail);

			// Click event for when user clicks sign up

			mBtnSignUp.Click += MBtnSignUp_Click;

			return view;
		}

        // Occurs when user clicks Sign up Button

        /// <summary>
        /// Completes Sign up process and passes arguments. 
        /// </summary>
        /// <param name="sender">The object that invoked the event</param>
        /// <param name="e">The event arguments</param>
        void MBtnSignUp_Click (object sender, EventArgs e)
		{
			if (mTxtUserName.Text != "" && mTxtPassConfirm.Text != "" && mTxtPassword.Text != "") {
				if (mTxtPassword.Text.Equals (mTxtPassConfirm.Text)) {
					mOnSignUpComplete.Invoke (this, new OnSignUpEventArgs (mTxtUserName.Text, mTxtPassConfirm.Text, mTxtPassword.Text));
					this.Dismiss ();
				} 
			}
		}

		/// <param name="savedInstanceState">If the fragment is being re-created from
		///  a previous saved state, this is the state.</param>
		/// <summary>
		/// Called when the fragment's activity has been created and this
		///  fragment's view hierarchy instantiated.
		/// </summary>
		public override void OnActivityCreated (Bundle savedInstanceState)
		{
			Dialog.Window.RequestFeature (WindowFeatures.NoTitle);
			base.OnActivityCreated (savedInstanceState);
		}
	}
}