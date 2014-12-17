using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Windows.Forms;

namespace ComExp.Visualization
{
	public class PlotHost
	{
		public static Form Main { get; private set; }

		public void AccomodateControl(Control what, string where = "Main")
		{
			GetForm(where).Controls.Add(what);
		}

		public void ShowForm(string name)
		{
			Invoke(() => ShowForm(name));
			Forms[name].Visible = true;
		}

		public void HideForm(string name)
		{
			Invoke(() => HideForm(name));
			Forms[name].Visible = false;
		}

		public PlotHost()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			Forms = new ConcurrentDictionary<string, Form>();
		}

		public Form GetForm(string name)
		{
			if (!Forms.ContainsKey(name))
			{
				Invoke(() => GetForm(name));
				if (!Main.InvokeRequired)
				{
					CreateForm(name);
				}
			}

			var ans = Forms[name];
			return ans;
		}

		private Form CreateForm(string name)
		{
			var RawForm = new Form { Text = name, Visible = true };
			
			Forms.AddOrUpdate(name, RawForm, (s, form) => RawForm);

			return RawForm;
		}

		public void Initialize()
		{
			Main = CreateForm("Main");
			Main.Invalidated += (sender, args) => InitializedEvent.Set();
		}

		private void Invoke(Action act)
		{
			if (Main.InvokeRequired)
				Main.BeginInvoke(act).AsyncWaitHandle.WaitOne();
		}

		private void SetDimensions(Form form)
		{

		}

		public void StartUp()
		{

			Initialize();
		}

		public ManualResetEvent InitializedEvent = new ManualResetEvent(false);
		private readonly ConcurrentDictionary<string, Form> Forms = new ConcurrentDictionary<string, Form>();
	}
}
