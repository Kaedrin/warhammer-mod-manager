using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using KMM.Steam;
using Microsoft.Win32;
using PackFileManager;
using Steamworks;
using Warhammer2MM.Properties;

namespace Warhammer2MM
{
	public class FormMain : Form
	{
		public enum ColorScheme
		{
			light,
			dark
		}

		private ColorScheme currentTheme;

		public bool bypassConflictDetect = true;

		public bool forceSteamDownloads = true;

		public bool darkMode = true;

		public bool deleteWhileForcingSteamDownloads;

		private ConcurrentBag<Pack> _vfxPacks;

		public bool backupMods;

		public bool copyContentToData = true;

		public bool highlightRecentMods = true;

		public DateTime _LastPatch = new DateTime(2021, 7, 14, 10, 0, 0);

		public DateTime _LastPatchWar2 = new DateTime(2021, 11, 3, 9, 0, 0);

		public DateTime _LastPatch3K = new DateTime(2023, 1, 7, 9, 0, 0);

		public DateTime _LastPatchDynamic;

		public DateTime _LastPatchWar3 = new DateTime(2023, 4, 13, 9, 0, 0);

		public DateTime _LastPatchTroy = new DateTime(2021, 12, 13, 9, 0, 0);

		private bool beingReset;

		private bool useLastProfile = true;

		private string profileNameUsed;

		private ListViewColumnSorter lvwColumnSorter;

		public Preferences corePrefs = new Preferences();

		private Dictionary<string, Tuple<string, string, string, string, string, string>> steamDictionary2;

		private Dictionary<string, SteamData> steamDictionary;

		private static readonly List<string> caPackList = new List<string>();

		private bool bypassCALauncher;

		private string exeName;

		private string lastChecked;

		private string tww2Path;

		private string tww2DataPath;

		private string xmlDataFileName;

		private string exePath;

		private List<Pack> modPacks = new List<Pack>();

		private bool SteamWorkshopIntegration = true;

		public List<string> LoadedMods = new List<string>();

		public StreamWriter DebugFileWriter;

		private ConcurrentBag<string> _DebugFileStrings = new ConcurrentBag<string>();

		private uint m_pcbTicket;

		private byte[] m_Ticket;

		private HAuthTicket m_HAuthTicket;

		protected Callback<GetAuthSessionTicketResponse_t> m_GetAuthSessionTicketResponse;

		public ConcurrentDictionary<ulong, string> steamUserNames;

		public volatile int downloadCount;

		private static readonly DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		private CallResult<SteamUGCQueryCompleted_t> OnSteamUGCQueryCompletedCallResult;

		private IContainer components;

		private Button deleteProfileButton;

		private Button saveProfileButton;

		private Label lbl_profiles;

		private ComboBox profilesComboBox;

		private Button moveDownButton;

		private Button moveUpButton;

		private Button launchGameButton;

		private Label labelMod;

		private GroupBox groupBoxProfiles;

		private Label labelOrderInfo;

		private FolderBrowserDialog findW2TWPathFolderBrowserDialog;

		private MenuStrip menuStrip;

		private ToolStripMenuItem functionsToolStripMenuItem;

		private ToolStripMenuItem informationToolStripMenuItem;

		private ToolStripMenuItem launchGameToolStripMenuItem;

		private ToolStripMenuItem revertToVanillaToolStripMenuItem;

		private ToolStripSeparator toolStripMenuItem1;

		private ToolStripMenuItem exitToolStripMenuItem;

		private Label labelDataPath;

		private ToolStripMenuItem selectGameDirectoryToolStripMenuItem;

		private ToolStripSeparator toolStripMenuItem2;

		private ToolStripMenuItem optionsToolStripMenuItem;

		private ToolStripMenuItem steamWorkshopIntegrationBETAToolStripMenuItem;

		private CheckBox BumpPriority;

		private ToolTip toolTip1;

		private PictureBox pictureBox1;

		private Button refreshManagerButton;

		private ColumnHeader nameColumn;

		private ColumnHeader conflictColumn;

		private ColumnHeader DateColumn;

		private ListView listViewMod;

		private Label label2;

		private CheckBox enableAllCheckBox;

		private ToolStripMenuItem refreshMenuItem;

		private ToolStripMenuItem profilesToolStripMenuItem;

		private ToolStripMenuItem exportProfileToolStripMenuItem;

		private ToolStripMenuItem importProfileToolStripMenuItem;

		private ToolStripMenuItem aboutWarhammerModManager;

		private ToolStripMenuItem contactToolStripMenuItem;

		private ToolStripMenuItem dataPath;

		private ToolStripMenuItem readmeToolStripMenuItem;

		private ToolStripMenuItem gameToolStripMenuItem;

		private ToolStripMenuItem warhammer2Game;

		private ToolStripMenuItem warhammerGame;

		private ToolStripMenuItem useLastProfileMenuItem;

		private ToolStripMenuItem remberLastGUILayoutToolStripMenuItem;

		private OpenFileDialog importProfile;

		private SaveFileDialog exportProfile;

		private Label labelModCount;

		private Label labelModCountNum;

		private ToolStripMenuItem gameAttila;

		private ToolStripMenuItem gameRome2;

		private ToolStripMenuItem gameEmpire;

		private ToolStripMenuItem gameNapoleon;

		private ToolStripMenuItem gameShogun2;

		private ToolStripMenuItem throbGame;

		private Button cleanDataButton;

		private ToolStripMenuItem cleanDataToolStripMenuItem;

		private ToolStripMenuItem highlightRecentModsMenuItem;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem showContentsToolStripMenuItem;

		private ToolStripMenuItem openWithPFMToolStripMenuItem;

		private ToolStripMenuItem openWithRPFMToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripMenuItem deleteDataToolStripMenuItem;

		private ToolStripMenuItem deleteContentStripMenuItem3;

		private ToolStripMenuItem copyToolStripMenuItem;

		private ToolStripMenuItem renameToolStripMenuItem;

		private ToolStripMenuItem setReadOnlyToolStripMenuItem;

		private ToolStripMenuItem setUserNote2StripMenuItem3;

		private ToolStripMenuItem SteamStripMenuItem;

		private ToolStripMenuItem downloadModAgainToolStripMenuItem;

		private ToolStripMenuItem voteUpToolStripMenuItem;

		private ToolStripMenuItem openSteamPageToolStripMenuItem;

		private ToolStripMenuItem updateModforModAuthorsToolStripMenuItem;

		private ToolStripMenuItem uploadModToolStripMenuItem;

		private ToolStripMenuItem resubscribeToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripMenuItem saveToArchiveToolStripMenuItem;

		private ToolStripMenuItem restoreFromArchiveToolStripMenuItem;

		private ToolStripMenuItem versionDatedXToolStripMenuItem;

		private ToolStripMenuItem versionDatedYToolStripMenuItem;

		private ToolStripMenuItem versionDatedZToolStripMenuItem;

		private ToolStripMenuItem hideModFromProfileToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripMenuItem addModToExistingProfileToolStripMenuItem;

		private ToolStripMenuItem backupModsToolStripMenuItem;

		private ToolStripMenuItem chooseBackupLocationToolStripMenuItem;

		private ToolStripMenuItem copyContentToDataToolStripMenuItem;

		private ToolStripTextBox toolStripTextBox1;

		private ToolStripMenuItem choosePackFileManagerLocationToolStripMenuItem;

		private ToolStripMenuItem chooseRustysPackFileManagerLocationToolStripMenuItem;

		private OpenFileDialog openPackFileDialog;

		private Label label1;

		private ToolStripMenuItem detectGameInstallationsToolStripMenuItem;

		private ToolStripMenuItem bypassCALauncherBetaToolStripMenuItem;

		private ToolStripMenuItem openSteamPage2ToolStripMenuItem;

		private LinkLabel linkLabel1;

		private ToolStripMenuItem openSteamPage3ToolStripMenuItem;

		private ToolStripMenuItem mergeSelectedModsToolStripMenuItem;

		private Label label3;

		private Label labelEnabledMovies;

		private ToolStripSeparator toolStripSeparator5;

		private ToolStripMenuItem unsubscribeToolStripMenuItem;

		private ToolStripMenuItem bypassConflictDetectionToolStripMenuItem;

		private ToolStripMenuItem threeKingdomsGame;

		private ToolStripMenuItem exportProfileToClipboardToolStripMenuItem;

		private ToolStripMenuItem exportProfileWithSteamXMLToolStripMenuItem;

		private ToolStripMenuItem exportAllModsWithSteamURLsToolStripMenuItem;

		private ToolStripMenuItem unsubscribeMultipleToolStripMenuItem;

		private ToolStripMenuItem deleteDataMultipleToolStripMenuItem;

		private ToolStripMenuItem troyGame;

		private ToolStripMenuItem enableDarkModeToolStripMenuItem;

		private ToolStripMenuItem exportCheckedModsToHTMLToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator6;

		private ToolStripMenuItem cleanDataAndExitApplicationToolStripMenuItem;

		private ToolStripMenuItem openContentFolderInFileExplorerToolStripMenuItem;

		private ToolStripMenuItem openDataFolderInFileExplorerToolStripMenuItem;

		private ToolStripMenuItem enableForceSteamDownloadsToolStripMenuItem;

		private ToolStripMenuItem enableDeleteWhileForcingSteamDownloadsToolStripMenuItem;

		private ToolStripMenuItem userScriptExtensionsToolStripMenuItem;

		private ToolStripMenuItem expandedDebugSettingsToolStripMenuItem;

		private ToolStripMenuItem warhammer3Game;

		[Conditional("DEBUG")]
		private void DebugLogException(Exception ex)
		{
			DebugFileWriter.WriteLine("=>{0} An Error occurred: {1}  Message (Stack/Message: {2}{3}", DateTime.Now, ex.StackTrace, Environment.NewLine, ex.Message, Environment.NewLine);
		}

		[Conditional("DEBUG")]
		private void DebugLogMessage(string debugMessage)
		{
			DebugFileWriter.WriteLine(debugMessage);
		}

		[Conditional("DEBUG")]
		private void DebugLogMessageFromBag()
		{
			foreach (string debugFileString in _DebugFileStrings)
			{
				DebugFileWriter.WriteLine(debugFileString);
			}
			_DebugFileStrings = new ConcurrentBag<string>();
		}

		[Conditional("DEBUG")]
		private void DebugAddMessageToBag(string debugMessage)
		{
			_DebugFileStrings.Add(debugMessage);
		}

		[Conditional("DEBUG")]
		private void DebugInitWriter()
		{
			DebugFileWriter = new StreamWriter(CoreData.debugFilePath, true);
			DebugFileWriter.WriteLine(DateTime.Now);
		}

		[Conditional("DEBUG")]
		private void DebugDisposeWriter()
		{
			DebugFileWriter.Dispose();
		}

		public void Init()
		{
		}

		public void LoadInstallPaths()
		{
			CheckScriptDirectory();
			if (!File.Exists(CoreData.installPaths))
			{
				return;
			}
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(CoreData.installPaths);
			foreach (XmlNode childNode in xmlDocument.DocumentElement.FirstChild.ChildNodes)
			{
				foreach (XmlAttribute attribute in childNode.Attributes)
				{
					if (attribute.Name == "ExePath" && !string.IsNullOrEmpty(attribute.InnerText))
					{
						CoreData.InstallLocations.Add(attribute.InnerText);
					}
				}
			}
		}

		private void OnGetAuthSessionTicketResponse(GetAuthSessionTicketResponse_t pCallback)
		{
		}

		public void WriteInstallPaths()
		{
			if (!File.Exists(CoreData.appIdPath))
			{
				Encoding encoding = new UTF8Encoding(false);
				using (StreamWriter streamWriter = new StreamWriter(CoreData.appIdPath, false, encoding))
				{
					streamWriter.WriteLine("480");
					streamWriter.Close();
				}
			}
			if (File.Exists(CoreData.appIdPath))
			{
				if (!SteamAPIWrapper.Init())
				{
					MessageBox.Show("Steam must be running in order for this feature to work. The manager will now exit. Please start Steam and then start the manager.");
					Application.Exit();
					Environment.Exit(0);
				}
				if (!SteamAPI.IsSteamRunning())
				{
					MessageBox.Show("Steam must be running the first time to determine the game installation paths.");
				}
				AppId_t appID = new AppId_t(480u);
				string text = "";
				XmlDocument xmlDocument = new XmlDocument();
				XmlNode newChild = xmlDocument.CreateXmlDeclaration("1.0", "UTF-8", null);
				xmlDocument.AppendChild(newChild);
				XmlNode xmlNode = xmlDocument.CreateElement("GameData");
				XmlNode xmlNode2 = xmlDocument.CreateElement("GameList");
				List<string> list = new List<string>();
				list.Add("594570");
				list.Add("364360");
				list.Add("712100");
				list.Add("325610");
				list.Add("10500");
				list.Add("214950");
				list.Add("34330");
				list.Add("34030");
				list.Add("779340");
				list.Add("1099410");
				list.Add("1142710");
				for (int i = 0; i < list.Count; i++)
				{
					CoreData.SetGame(list[i]);
					XmlNode xmlNode3 = xmlDocument.CreateElement("Game");
					xmlNode3.InnerText = CoreData.appIDString;
					XmlAttribute xmlAttribute = xmlDocument.CreateAttribute("ExePath");
					appID.m_AppId = Convert.ToUInt32(list[i]);
					text = "";
					SteamApps.GetAppInstallDir(appID, out text, 300u);
					if (!Directory.Exists(text))
					{
						text = "";
					}
					xmlAttribute.Value = text;
					xmlNode3.Attributes.Append(xmlAttribute);
					xmlAttribute = xmlDocument.CreateAttribute("ExeName");
					xmlAttribute.Value = CoreData.exeName;
					xmlNode3.Attributes.Append(xmlAttribute);
					xmlAttribute = xmlDocument.CreateAttribute("AppDataGameName");
					xmlAttribute.Value = CoreData.appDataGameName;
					xmlNode3.Attributes.Append(xmlAttribute);
					xmlNode2.AppendChild(xmlNode3);
				}
				xmlNode.AppendChild(xmlNode2);
				xmlDocument.AppendChild(xmlNode);
				xmlDocument.Save(CoreData.installPaths);
				MessageBox.Show("Detecting game installations for the first time, after which the mod manager will need to exit. This will only be done once or if you use the revert to vanilla option.", "First time run detected!");
				try
				{
					SteamAPIWrapper.Shutdown();
					SteamAPI.ReleaseCurrentThreadMemory();
				}
				catch (Exception)
				{
				}
				if (File.Exists(CoreData.appIdPath))
				{
					File.Delete(CoreData.appIdPath);
				}
			}
			Application.Exit();
			Environment.Exit(0);
		}

		private string StripText(string text, string remove)
		{
			if (text.Trim().StartsWith(remove))
			{
				int num = text.IndexOf(remove);
				if (num >= 0)
				{
					text = text.Substring(num + remove.Length);
				}
			}
			return text;
		}

		public void ChangeTheme(ColorScheme scheme, Control.ControlCollection container)
		{
			BackColor = Color.FromArgb(51, 51, 51);
			ForeColor = Color.Wheat;
			contextMenuStrip1.BackColor = Color.FromArgb(51, 51, 51);
			contextMenuStrip1.ForeColor = Color.Wheat;
			functionsToolStripMenuItem.DropDown.BackColor = Color.FromArgb(51, 51, 51);
			functionsToolStripMenuItem.DropDown.ForeColor = Color.Wheat;
			optionsToolStripMenuItem.DropDown.BackColor = Color.FromArgb(51, 51, 51);
			optionsToolStripMenuItem.DropDown.ForeColor = Color.Wheat;
			profilesToolStripMenuItem.DropDown.BackColor = Color.FromArgb(51, 51, 51);
			profilesToolStripMenuItem.DropDown.ForeColor = Color.Wheat;
			informationToolStripMenuItem.DropDown.BackColor = Color.FromArgb(51, 51, 51);
			informationToolStripMenuItem.DropDown.ForeColor = Color.Wheat;
			gameToolStripMenuItem.DropDown.BackColor = Color.FromArgb(51, 51, 51);
			gameToolStripMenuItem.DropDown.ForeColor = Color.Wheat;
			(functionsToolStripMenuItem.DropDown as ToolStripDropDownMenu).ShowImageMargin = false;
			(informationToolStripMenuItem.DropDown as ToolStripDropDownMenu).ShowImageMargin = false;
			(profilesToolStripMenuItem.DropDown as ToolStripDropDownMenu).ShowImageMargin = false;
			linkLabel1.LinkColor = Color.SkyBlue;
			foreach (Control item in container)
			{
				if (item is Panel)
				{
					if (scheme == ColorScheme.dark)
					{
						item.BackColor = Color.FromArgb(51, 51, 51);
						item.ForeColor = Color.Wheat;
						Font font = item.Font;
					}
					ChangeTheme(scheme, item.Controls);
				}
				else if (scheme == ColorScheme.dark)
				{
					item.BackColor = Color.FromArgb(51, 51, 51);
					item.ForeColor = Color.Wheat;
					Font font2 = item.Font;
					if (item.Controls.Count > 0)
					{
						ChangeTheme(scheme, item.Controls);
					}
				}
			}
		}

		private const string UpdateDotTag = "__update_available__";

		// Draw a red dot after the date for mods that have a Steam update available.
		// Every other cell is drawn by Windows exactly as before.
		private void listViewMod_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
		{
			e.DrawDefault = true;
		}

		private void listViewMod_DrawItem(object sender, DrawListViewItemEventArgs e)
		{
			if (listViewMod.View != View.Details)
			{
				e.DrawDefault = true;
			}
		}

		private void listViewMod_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
		{
			if (e.SubItem == null || !(e.SubItem.Tag is string) || (string)e.SubItem.Tag != UpdateDotTag)
			{
				e.DrawDefault = true;
				return;
			}
			bool selected = e.Item.Selected;
			// Theme colors are stored with alpha 0 (Windows ignores alpha); force opaque for GDI+.
			Color back = Color.FromArgb(255, selected ? SystemColors.Highlight : e.SubItem.BackColor);
			Color fore = Color.FromArgb(255, selected ? SystemColors.HighlightText : e.SubItem.ForeColor);
			using (SolidBrush brush = new SolidBrush(back))
			{
				e.Graphics.FillRectangle(brush, e.Bounds);
			}
			Rectangle textRect = new Rectangle(e.Bounds.X + 6, e.Bounds.Y, Math.Max(0, e.Bounds.Width - 22), e.Bounds.Height);
			TextFormatFlags tf = TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix | TextFormatFlags.SingleLine;
			TextRenderer.DrawText(e.Graphics, e.SubItem.Text, e.SubItem.Font, textRect, fore, tf);
			Size textSize = TextRenderer.MeasureText(e.Graphics, e.SubItem.Text, e.SubItem.Font, textRect.Size, tf);
			int d = Math.Max(6, Math.Min(9, e.Bounds.Height - 8));
			int x = Math.Min(textRect.X + textSize.Width + 4, e.Bounds.Right - d - 3);
			int y = e.Bounds.Y + (e.Bounds.Height - d) / 2;
			System.Drawing.Drawing2D.SmoothingMode old = e.Graphics.SmoothingMode;
			e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
			using (SolidBrush red = new SolidBrush(Color.FromArgb(230, 40, 40)))
			{
				e.Graphics.FillEllipse(red, x, y, d, d);
			}
			e.Graphics.SmoothingMode = old;
		}

		// ---- Desktop shortcut that launches the selected game with the currently checked mods ----
		// The mod list is written to a separate script file in the game folder (the same "mod \"x.pack\";"
		// format as user.script.txt) because user.script.txt is deleted when the manager closes.
		// The shortcut passes that file to the game exe, the same way the CA launcher passes used_mods.txt.
		private const string ShortcutModListFile = "kmm_shortcut_mods.txt";

		private Button createShortcutButton;

		private ToolStripMenuItem createShortcutToolStripMenuItem;

		private void AddCreateShortcutControls()
		{
			createShortcutButton = new Button();
			createShortcutButton.Font = new Font("Verdana", 7.5f, FontStyle.Regular, GraphicsUnit.Point, 0);
			createShortcutButton.Location = new Point(refreshManagerButton.Right + 6, refreshManagerButton.Top);
			createShortcutButton.Size = new Size(launchGameButton.Right - refreshManagerButton.Right - 6, refreshManagerButton.Height);
			createShortcutButton.Name = "createShortcutButton";
			createShortcutButton.Text = "Desktop\nShortcut";
			createShortcutButton.UseVisualStyleBackColor = true;
			createShortcutButton.Click += createShortcut_Click;
			new ToolTip().SetToolTip(createShortcutButton, "Create a desktop shortcut that starts the game directly with the currently checked mods.");
			Controls.Add(createShortcutButton);
			createShortcutToolStripMenuItem = new ToolStripMenuItem();
			createShortcutToolStripMenuItem.Name = "createShortcutToolStripMenuItem";
			createShortcutToolStripMenuItem.Text = "Create &Desktop Shortcut (with checked mods)";
			createShortcutToolStripMenuItem.Click += createShortcut_Click;
			functionsToolStripMenuItem.DropDownItems.Insert(functionsToolStripMenuItem.DropDownItems.IndexOf(launchGameToolStripMenuItem) + 1, createShortcutToolStripMenuItem);
		}

		private void createShortcut_Click(object sender, EventArgs e)
		{
			string exeFile = string.IsNullOrEmpty(CoreData.exePath) ? null : Path.Combine(CoreData.exePath, CoreData.exeName);
			if (exeFile == null || !File.Exists(exeFile))
			{
				MessageBox.Show("The game executable was not found. Please check the game directory first.", "Create Desktop Shortcut", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			List<string> mods = ListActivatedMods();
			try
			{
				Encoding encoding = new UTF8Encoding(false);
				using (StreamWriter streamWriter = new StreamWriter(Path.Combine(CoreData.exePath, ShortcutModListFile), false, encoding))
				{
					foreach (string mod in mods)
					{
						streamWriter.WriteLine("mod \"" + mod + "\";");
					}
				}
				WriteProfile("LastUsedMods");
				string gameName = Path.GetFileName(CoreData.exePath.TrimEnd('\\'));
				string shortcutPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), gameName + " (KMM mods).lnk");
				CreateShortcutFile(shortcutPath, exeFile, ShortcutModListFile + ";", CoreData.exePath, exeFile + ",0", gameName + " - launch with " + mods.Count + " mods selected in Kaedrin's Mod Manager");
				MessageBox.Show("Desktop shortcut created:\n" + shortcutPath + "\n\nMods: " + mods.Count + "\n\nClick this button again after changing the checked mods or their order to update the shortcut.", "Create Desktop Shortcut", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Could not create the desktop shortcut:\n" + ex.Message, "Create Desktop Shortcut", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		// Uses the WScript.Shell COM object via late binding so no extra assembly references are needed.
		private static void CreateShortcutFile(string shortcutPath, string target, string arguments, string workingDirectory, string iconLocation, string description)
		{
			Type shellType = Type.GetTypeFromProgID("WScript.Shell");
			object shell = Activator.CreateInstance(shellType);
			try
			{
				object shortcut = shellType.InvokeMember("CreateShortcut", BindingFlags.InvokeMethod, null, shell, new object[1] { shortcutPath });
				Type t = shortcut.GetType();
				t.InvokeMember("TargetPath", BindingFlags.SetProperty, null, shortcut, new object[1] { target });
				t.InvokeMember("Arguments", BindingFlags.SetProperty, null, shortcut, new object[1] { arguments });
				t.InvokeMember("WorkingDirectory", BindingFlags.SetProperty, null, shortcut, new object[1] { workingDirectory });
				t.InvokeMember("IconLocation", BindingFlags.SetProperty, null, shortcut, new object[1] { iconLocation });
				t.InvokeMember("Description", BindingFlags.SetProperty, null, shortcut, new object[1] { description });
				t.InvokeMember("Save", BindingFlags.InvokeMethod, null, shortcut, null);
				Marshal.FinalReleaseComObject(shortcut);
			}
			finally
			{
				Marshal.FinalReleaseComObject(shell);
			}
		}

		// ---- Drag & drop reordering of the mod list (same effect as the up/down arrow buttons) ----
		private int dragInsertIndex = -1;

		private void EnableDragDropReorder()
		{
			listViewMod.AllowDrop = true;
			listViewMod.ItemDrag += listViewMod_ItemDrag;
			listViewMod.DragEnter += listViewMod_DragOver;
			listViewMod.DragOver += listViewMod_DragOver;
			listViewMod.DragLeave += listViewMod_DragLeave;
			listViewMod.DragDrop += listViewMod_DragDrop;
		}

		private void listViewMod_ItemDrag(object sender, ItemDragEventArgs e)
		{
			if (e.Button == MouseButtons.Left && listViewMod.SelectedItems.Count > 0)
			{
				listViewMod.DoDragDrop(typeof(FormMain).FullName, DragDropEffects.Move);
			}
		}

		// Index the dragged items will be inserted before (0..Count).
		private int GetDropIndex(Point screenPoint)
		{
			Point p = listViewMod.PointToClient(screenPoint);
			ListViewItem target = listViewMod.GetItemAt(4, p.Y);
			if (target == null)
			{
				if (listViewMod.Items.Count == 0)
				{
					return 0;
				}
				ListViewItem top = listViewMod.TopItem;
				return (top != null && p.Y < top.Bounds.Top) ? top.Index : listViewMod.Items.Count;
			}
			Rectangle b = target.Bounds;
			return (p.Y < b.Top + b.Height / 2) ? target.Index : target.Index + 1;
		}

		private void listViewMod_DragOver(object sender, DragEventArgs e)
		{
			if (!e.Data.GetDataPresent(DataFormats.StringFormat) || (string)e.Data.GetData(DataFormats.StringFormat) != typeof(FormMain).FullName)
			{
				e.Effect = DragDropEffects.None;
				return;
			}
			e.Effect = DragDropEffects.Move;
			// auto-scroll near the top/bottom edge
			Point p = listViewMod.PointToClient(new Point(e.X, e.Y));
			ListViewItem hover = listViewMod.GetItemAt(4, p.Y);
			if (hover != null)
			{
				if (p.Y < hover.Bounds.Height * 2 && hover.Index > 0)
				{
					listViewMod.EnsureVisible(hover.Index - 1);
				}
				else if (p.Y > listViewMod.ClientSize.Height - hover.Bounds.Height * 2 && hover.Index < listViewMod.Items.Count - 1)
				{
					listViewMod.EnsureVisible(hover.Index + 1);
				}
			}
			int index = GetDropIndex(new Point(e.X, e.Y));
			if (index != dragInsertIndex)
			{
				dragInsertIndex = index;
				listViewMod.Invalidate();
				listViewMod.Update();
			}
			DrawInsertionLine();
		}

		private void DrawInsertionLine()
		{
			if (dragInsertIndex < 0 || listViewMod.Items.Count == 0)
			{
				return;
			}
			int y;
			if (dragInsertIndex < listViewMod.Items.Count)
			{
				y = listViewMod.Items[dragInsertIndex].Bounds.Top;
			}
			else
			{
				y = listViewMod.Items[listViewMod.Items.Count - 1].Bounds.Bottom - 1;
			}
			using (Graphics g = listViewMod.CreateGraphics())
			using (Pen pen = new Pen(Color.FromArgb(255, 45, 85), 3f))
			{
				g.DrawLine(pen, 0, y, listViewMod.ClientSize.Width, y);
				g.FillPolygon(Brushes.White, new Point[3] { new Point(0, y - 5), new Point(6, y), new Point(0, y + 5) });
			}
		}

		private void listViewMod_DragLeave(object sender, EventArgs e)
		{
			dragInsertIndex = -1;
			listViewMod.Invalidate();
		}

		private void listViewMod_DragDrop(object sender, DragEventArgs e)
		{
			int index = GetDropIndex(new Point(e.X, e.Y));
			dragInsertIndex = -1;
			List<ListViewItem> moving = listViewMod.SelectedItems.Cast<ListViewItem>().OrderBy((ListViewItem i) => i.Index).ToList();
			if (moving.Count == 0)
			{
				listViewMod.Invalidate();
				return;
			}
			// insert position after the moving items are removed
			int insertAt = index - moving.Count((ListViewItem i) => i.Index < index);
			List<bool> checkedStates = moving.Select((ListViewItem i) => i.Checked).ToList();
			listViewMod.BeginUpdate();
			try
			{
				foreach (ListViewItem item in moving)
				{
					listViewMod.Items.Remove(item);
				}
				listViewMod.SelectedItems.Clear();
				for (int j = 0; j < moving.Count; j++)
				{
					listViewMod.Items.Insert(insertAt + j, moving[j]);
					listViewMod.Items[insertAt + j].Checked = checkedStates[j];
					listViewMod.Items[insertAt + j].Selected = true;
				}
			}
			finally
			{
				listViewMod.EndUpdate();
			}
			listViewMod.EnsureVisible(insertAt);
			listViewMod.Invalidate();
			listViewMod.Focus();
		}

		public FormMain()
		{
			InitializeComponent();
			listViewMod.OwnerDraw = true;
			listViewMod.DrawColumnHeader += listViewMod_DrawColumnHeader;
			listViewMod.DrawItem += listViewMod_DrawItem;
			listViewMod.DrawSubItem += listViewMod_DrawSubItem;
			AddCreateShortcutControls();
			EnableDragDropReorder();
			if (currentTheme == ColorScheme.dark)
			{
				ChangeTheme(currentTheme, Controls);
			}
			Init();
			Text = "Kaedrin's Mod Manager v1.9.0";
			bypassCALauncher = bypassCALauncherBetaToolStripMenuItem.Checked;
			CheckScriptDirectory();
			if (File.Exists(CoreData.installPaths))
			{
				LoadInstallPaths();
			}
			else
			{
				WriteInstallPaths();
			}
			if (File.Exists(CoreData.appIdPath))
			{
				File.Delete(CoreData.appIdPath);
			}
			CoreData.SetGame("364360");
			if (CoreData.GetGameDirectory() == null)
			{
				warhammerGame.Enabled = false;
			}
			CoreData.SetGame("325610");
			if (CoreData.GetGameDirectory() == null)
			{
				gameAttila.Enabled = false;
			}
			CoreData.SetGame("10500");
			if (CoreData.GetGameDirectory() == null)
			{
				gameEmpire.Enabled = false;
			}
			CoreData.SetGame("214950");
			if (CoreData.GetGameDirectory() == null)
			{
				gameRome2.Enabled = false;
			}
			CoreData.SetGame("34330");
			if (CoreData.GetGameDirectory() == null)
			{
				gameShogun2.Enabled = false;
			}
			CoreData.SetGame("34030");
			if (CoreData.GetGameDirectory() == null)
			{
				gameNapoleon.Enabled = false;
			}
			CoreData.SetGame("712100");
			if (CoreData.GetGameDirectory() == null)
			{
				throbGame.Enabled = false;
			}
			CoreData.SetGame("779340");
			if (CoreData.GetGameDirectory() == null)
			{
				threeKingdomsGame.Enabled = false;
			}
			CoreData.SetGame("1099410");
			if (CoreData.GetGameDirectory() == null)
			{
				troyGame.Enabled = false;
			}
			CoreData.SetGame("594570");
			if (CoreData.GetGameDirectory() == null)
			{
				warhammer2Game.Enabled = false;
			}
			CoreData.SetGame("1142710");
			if (CoreData.GetGameDirectory() == null)
			{
				warhammer3Game.Checked = false;
				warhammer3Game.Enabled = false;
				bool flag = false;
				foreach (ToolStripMenuItem dropDownItem in gameToolStripMenuItem.DropDownItems)
				{
					if (dropDownItem.Enabled)
					{
						if (!flag)
						{
							dropDownItem.Checked = true;
						}
					}
					else
					{
						dropDownItem.Checked = false;
					}
				}
			}
			else
			{
				warhammer3Game.Checked = true;
			}
			lvwColumnSorter = new ListViewColumnSorter();
			lvwColumnSorter.Order = SortOrder.Ascending;
			listViewMod.ListViewItemSorter = lvwColumnSorter;
			LoadSettings();
			if (warhammer2Game.Checked)
			{
				exeName = "Warhammer2.exe";
			}
			else
			{
				exeName = "Warhammer.exe";
			}
		}

		protected virtual bool IsFileLocked(FileInfo file)
		{
			FileStream fileStream = null;
			try
			{
				fileStream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
			}
			catch (IOException)
			{
				return true;
			}
			finally
			{
				if (fileStream != null)
				{
					fileStream.Close();
				}
			}
			return false;
		}

		public void CopyFile(string currentFile, string dataFile)
		{
			try
			{
				using (File.Open(currentFile, FileMode.Open, FileAccess.Read, FileShare.Read))
				{
					File.Copy(currentFile, dataFile);
				}
			}
			catch
			{
			}
		}

		private void HandleFullSteamData2(string contentPath)
		{
			Encoding encoding = new UTF8Encoding(false);
			using (StreamWriter streamWriter = new StreamWriter(Application.StartupPath + "\\steam_appid.txt", false, encoding))
			{
				streamWriter.WriteLine(CoreData.appIDString);
				streamWriter.Close();
			}
			if (!SteamAPIWrapper.Init())
			{
				MessageBox.Show("Steam must be running in order for this feature to work. The manager will now exit. Please start Steam and then start the manager.");
				Application.Exit();
				Environment.Exit(0);
			}
			AppId_t appId_t = default(AppId_t);
			appId_t.m_AppId = uint.Parse(CoreData.appIDString);
			ConcurrentBag<SteamData> _steamData = new ConcurrentBag<SteamData>();
			ConcurrentBag<string> _steamBadData = new ConcurrentBag<string>();
			List<SteamData> list = new List<SteamData>();
			PublishedFileId_t[] array = new PublishedFileId_t[SteamUGC.GetNumSubscribedItems()];
			SteamUGC.GetSubscribedItems(array, SteamUGC.GetNumSubscribedItems());
			steamUserNames = new ConcurrentDictionary<ulong, string>(Environment.ProcessorCount * 2, 100);
			ConcurrentBag<SteamData> RemovedDeniedMods = new ConcurrentBag<SteamData>();
			DateTime checkTime = _LastPatch;
			if (CoreData.appDataGameName == "Warhammer2")
			{
				checkTime = _LastPatchWar2;
			}
			else if (CoreData.appDataGameName == "Warhammer3")
			{
				checkTime = _LastPatchWar3;
			}
			else if (CoreData.appDataGameName == "Troy")
			{
				checkTime = _LastPatchTroy;
			}
			else if (CoreData.appDataGameName == "ThreeKingdoms")
			{
				checkTime = _LastPatch3K;
			}
			double num = (double)checkTime.ToUnixTimestampSecs();
			bool outOfDateHaksExist = false;
			new ParallelOptions();
			Parallel.ForEach(array, (PublishedFileId_t currentID) =>
			{
				SteamUGCDetails_t details = Workshop.GetDetails(currentID.m_PublishedFileId);
				SteamData steamData = new SteamData();
				string text = Path.Combine(contentPath, currentID.m_PublishedFileId.ToString());
				if (Directory.Exists(text))
				{
					foreach (string item in new List<string>(Directory.EnumerateFiles(text)))
					{
						if (item.ToLower().EndsWith(".pack"))
						{
							steamData.HakFileName = item.Substring(item.LastIndexOf("\\") + 1);
							DateTime lastWriteTime = File.GetLastWriteTime(item);
							steamData.HakLastUpdated = lastWriteTime;
							steamData.ContentHakFile = Path.Combine(text, item);
						}
						else if (item.ToLower().EndsWith(".png"))
						{
							steamData.PNGFileName = item;
							DateTime lastWriteTime2 = File.GetLastWriteTime(item);
							steamData.PNGLaastUpdated = lastWriteTime2;
							steamData.ContentPNGFile = Path.Combine(text, item);
						}
						else if (details.m_pchFileName != "")
						{
							steamData.HakFileName = details.m_pchFileName;
							steamData.HakFileName = steamData.HakFileName.Remove(0, 5);
							steamData.PNGFileName = steamData.HakFileName.Replace(".pack", ".png");
							if (!File.Exists(Path.Combine(CoreData.dataPath, steamData.PNGFileName)))
							{
								steamData.PNGFileName = null;
							}
							steamData.BinFormat = true;
						}
						else
						{
							steamData.HakFileName = null;
						}
					}
				}
				else if (details.m_pchFileName != "")
				{
					steamData.HakFileName = details.m_pchFileName;
					steamData.HakFileName = steamData.HakFileName.Remove(0, 5);
					steamData.PNGFileName = steamData.HakFileName.Replace(".pack", ".png");
					if (!File.Exists(Path.Combine(CoreData.dataPath, steamData.PNGFileName)))
					{
						steamData.PNGFileName = null;
					}
				}
				else
				{
					steamData.SteamID = details.m_nPublishedFileId.m_PublishedFileId;
					RemovedDeniedMods.Add(steamData);
				}
				if (steamData.HakFileName != null)
				{
					string value = null;
					if (steamUserNames.TryGetValue(details.m_ulSteamIDOwner, out value))
					{
						steamData.SteamAuthor = value;
					}
					else
					{
						CSteamID cSteamID = new CSteamID(details.m_ulSteamIDOwner);
						SteamFriends.RequestUserInformation(cSteamID, true);
						value = SteamFriends.GetFriendPersonaName(cSteamID);
						if (value == "" && SteamUser.GetSteamID().m_SteamID != details.m_ulSteamIDOwner)
						{
							value = "Unknown Author/Removed Content";
						}
						steamData.SteamAuthor = value;
						if (value != "")
						{
							steamUserNames.TryAdd(details.m_ulSteamIDOwner, value);
						}
					}
					steamData.SteamTitle = details.m_rgchTitle;
					steamData.DisplayFileName = Path.GetFileName(steamData.HakFileName);
					steamData.SteamDescription = details.m_rgchDescription;
					steamData.SteamTags = details.m_rgchTags;
					steamData.SteamURL = details.m_rgchURL;
					steamData.UnixHakLastUpdated = details.m_rtimeUpdated;
					steamData.SteamID = details.m_nPublishedFileId.m_PublishedFileId;
					DateTime dateTime = FromUnixTime(details.m_rtimeUpdated);
					string path = Path.Combine(text, steamData.HakFileName);
					DateTime lastWriteTime3 = File.GetLastWriteTime(path);
					DateTime dateTime2 = dateTime.ToLocalTime();
					// Update-available detection: ask Steam directly, and also compare the
					// downloaded content file against the workshop upload time.
					try
					{
						uint itemState = SteamUGC.GetItemState(currentID);
						if ((itemState & (uint)(EItemState.k_EItemStateNeedsUpdate | EItemState.k_EItemStateDownloading | EItemState.k_EItemStateDownloadPending)) != 0)
						{
							steamData.UpdateAvailable = true;
						}
					}
					catch
					{
					}
					if (!steamData.BinFormat && File.Exists(path) && lastWriteTime3 < dateTime2)
					{
						steamData.UpdateAvailable = true;
					}
					if (lastWriteTime3 < dateTime2 && !steamData.BinFormat)
					{
						try
						{
							if (deleteWhileForcingSteamDownloads)
							{
								File.Delete(path);
							}
						}
						catch
						{
						}
						if (forceSteamDownloads)
						{
							Workshop.DownloadItem(details.m_nPublishedFileId.m_PublishedFileId);
						}
						outOfDateHaksExist = true;
					}
					steamData.SteamLastUploadDate = dateTime;
					if (dateTime < checkTime)
					{
						steamData.Outdated = true;
					}
					steamData.SteamURL = "https://steamcommunity.com/sharedfiles/filedetails/?id=" + details.m_nPublishedFileId.m_PublishedFileId;
					_steamData.Add(steamData);
				}
				else if (details.m_ulSteamIDOwner != SteamUser.GetSteamID().m_SteamID)
				{
					_steamBadData.Add("https://steamcommunity.com/sharedfiles/filedetails/?id=" + details.m_nPublishedFileId.m_PublishedFileId);
				}
			});
			list = _steamData.ToList();
			steamDictionary = new Dictionary<string, SteamData>();
			foreach (SteamData item2 in list)
			{
				if (!steamDictionary.ContainsKey(item2.DisplayFileName))
				{
					steamDictionary.Add(item2.DisplayFileName, item2);
				}
			}
			list.Clear();
			list = null;
			SteamAPI.Shutdown();
			File.Delete(Application.StartupPath + "\\steam_appid.txt");
			if (outOfDateHaksExist && forceSteamDownloads)
			{
				MessageBox.Show("Out of date mods detected and are downloading, when finished please click Refresh.");
			}
		}

		private void HandleContentFolder()
		{
			DateTime dateTime = _LastPatch;
			if (CoreData.appDataGameName == "Warhammer2")
			{
				dateTime = _LastPatchWar2;
			}
			else if (CoreData.appDataGameName == "Warhammer3")
			{
				dateTime = _LastPatchWar3;
			}
			else if (CoreData.appDataGameName == "Troy")
			{
				dateTime = _LastPatchTroy;
			}
			else if (CoreData.appDataGameName == "ThreeKingdoms")
			{
				dateTime = _LastPatch3K;
			}
			int length = tww2Path.IndexOf("common");
			string text = tww2Path.Substring(0, length) + "workshop\\content\\" + CoreData.appIDString;
			if (!Directory.Exists(text))
			{
				return;
			}
			List<string> list = new List<string>(Directory.EnumerateDirectories(text, "*.*", SearchOption.AllDirectories));
			if (steamDictionary == null || steamDictionary.Count == 0)
			{
				HandleFullSteamData2(text);
			}
			List<SteamData> list2 = new List<SteamData>();
			Encoding encoding = new UTF8Encoding(false);
			using (StreamWriter streamWriter = new StreamWriter(Application.StartupPath + "\\steam_appid.txt", false, encoding))
			{
				streamWriter.WriteLine(CoreData.appIDString);
				streamWriter.Close();
			}
			if (!SteamAPIWrapper.Init())
			{
				MessageBox.Show("Steam must be running in order for this feature to work. The manager will now exit. Please start Steam and then start the manager.");
				Application.Exit();
				Environment.Exit(0);
			}
			bool flag = false;
			foreach (string item in list)
			{
				List<string> list3 = new List<string>(Directory.EnumerateFiles(item));
				if (list3.Count == 0)
				{
					try
					{
						Directory.Delete(item);
					}
					catch
					{
						MessageBox.Show("Error trying to delete: " + item);
					}
				}
				else
				{
					foreach (string item2 in list3)
					{
						if (item2.EndsWith(".bin"))
						{
							continue;
						}
						DateTime lastWriteTime = File.GetLastWriteTime(item2);
						int num = item2.LastIndexOf("\\");
						string text2 = item2.Substring(num + 1);
						if (new FileInfo(item2).Length == 0L)
						{
							string text3 = item2.Substring(text.Length + 1, num - (text.Length + 1));
							MessageBox.Show("0.0 sized pack file detected: " + item2 + "\nResubscribe to the file and try again. \nThe URL is: https://steamcommunity.com/sharedfiles/filedetails/?id=" + text3);
							try
							{
								File.Delete(item2);
							}
							catch
							{
							}
							break;
						}
						string text4 = tww2DataPath + "\\" + text2;
						if (File.Exists(text4) && new FileInfo(text4).Length == 0L)
						{
							try
							{
								File.Delete(text4);
							}
							catch
							{
							}
						}
						if (File.Exists(text4))
						{
							DateTime lastWriteTime2 = File.GetLastWriteTime(text4);
							if (!(lastWriteTime2 > lastWriteTime) && !(lastWriteTime2 == lastWriteTime))
							{
								try
								{
									File.Delete(text4);
								}
								catch
								{
									MessageBox.Show("Error trying to delete: " + text4);
								}
								CopyFile(item2, text4);
								flag = true;
							}
						}
						else
						{
							CopyFile(item2, text4);
							flag = true;
						}
					}
				}
				if (!flag)
				{
					continue;
				}
				string text5 = "";
				SteamData steamData = new SteamData();
				foreach (string item3 in list3)
				{
					int num2 = item3.LastIndexOf("\\");
					string displayFileName = item3.Substring(num2 + 1);
					if (item3.EndsWith(".pack"))
					{
						text5 = item3;
						DateTime lastWriteTime3 = File.GetLastWriteTime(item3);
						steamData.HakLastUpdated = lastWriteTime3;
						steamData.DisplayFileName = displayFileName;
						steamData.HakFileName = item3;
					}
					else if (item3.EndsWith(".png"))
					{
						DateTime lastWriteTime4 = File.GetLastWriteTime(item3);
						steamData.PNGLaastUpdated = lastWriteTime4;
						steamData.PNGFileName = item3;
					}
				}
				if (text5 != "")
				{
					int num3 = text5.LastIndexOf("\\");
					string text6 = text5.Substring(num3 + 1);
					steamDictionary.Remove(text6);
					int num4 = text5.LastIndexOf(CoreData.appIDString);
					string text7 = text5.Substring(num4 + 1 + CoreData.appIDString.Length);
					string s = text7.Substring(0, text7.Length - text6.Length - 1);
					steamData.SteamID = ulong.Parse(s);
					SteamUGCDetails_t details = Workshop.GetDetails(steamData.SteamID);
					CSteamID cSteamID = new CSteamID(details.m_ulSteamIDOwner);
					SteamFriends.RequestUserInformation(cSteamID, true);
					string friendPersonaName = SteamFriends.GetFriendPersonaName(cSteamID);
					steamData.SteamTitle = details.m_rgchTitle;
					steamData.SteamAuthor = friendPersonaName;
					steamData.DisplayFileName = Path.GetFileName(steamData.HakFileName);
					steamData.SteamDescription = details.m_rgchDescription;
					steamData.SteamTags = details.m_rgchTags;
					steamData.SteamURL = details.m_rgchURL;
					if ((steamData.SteamLastUploadDate = FromUnixTime(details.m_rtimeUpdated)) < dateTime)
					{
						steamData.Outdated = true;
					}
					steamData.SteamURL = "https://steamcommunity.com/sharedfiles/filedetails/?id=" + steamData.SteamID;
					list2.Add(steamData);
					if (!steamDictionary.ContainsKey(steamData.DisplayFileName))
					{
						steamDictionary.Add(steamData.DisplayFileName, steamData);
					}
				}
				flag = false;
			}
			list2.Clear();
			list2 = null;
			SteamAPI.Shutdown();
			File.Delete(Application.StartupPath + "\\steam_appid.txt");
		}

		private void HandleContentFolderWithoutCopy()
		{
			DateTime dateTime = _LastPatch;
			if (CoreData.appDataGameName == "Warhammer2")
			{
				dateTime = _LastPatchWar2;
			}
			else if (CoreData.appDataGameName == "Warhammer3")
			{
				dateTime = _LastPatchWar3;
			}
			else if (CoreData.appDataGameName == "Troy")
			{
				dateTime = _LastPatchTroy;
			}
			else if (CoreData.appDataGameName == "ThreeKingdoms")
			{
				dateTime = _LastPatch3K;
			}
			int length = tww2Path.IndexOf("common");
			string text = tww2Path.Substring(0, length) + "workshop\\content\\" + CoreData.appIDString;
			if (!Directory.Exists(text))
			{
				return;
			}
			List<string> list = new List<string>(Directory.EnumerateDirectories(text, "*.*", SearchOption.AllDirectories));
			if (steamDictionary == null || steamDictionary.Count == 0)
			{
				HandleFullSteamData2(text);
			}
			List<SteamData> list2 = new List<SteamData>();
			Encoding encoding = new UTF8Encoding(false);
			using (StreamWriter streamWriter = new StreamWriter(Application.StartupPath + "\\steam_appid.txt", false, encoding))
			{
				streamWriter.WriteLine(CoreData.appIDString);
				streamWriter.Close();
			}
			if (!SteamAPIWrapper.Init())
			{
				MessageBox.Show("Steam must be running in order for this feature to work. The manager will now exit. Please start Steam and then start the manager.");
				Application.Exit();
				Environment.Exit(0);
			}
			bool flag = false;
			foreach (string item in list)
			{
				List<string> list3 = new List<string>(Directory.EnumerateFiles(item));
				if (list3.Count == 0)
				{
					try
					{
						Directory.Delete(item);
					}
					catch
					{
						MessageBox.Show("Error trying to delete: " + item);
					}
				}
				else
				{
					foreach (string item2 in list3)
					{
						if (item2.EndsWith(".bin"))
						{
							continue;
						}
						DateTime lastWriteTime = File.GetLastWriteTime(item2);
						int num = item2.LastIndexOf("\\");
						string text2 = item2.Substring(num + 1);
						if (new FileInfo(item2).Length == 0L)
						{
							string text3 = item2.Substring(text.Length + 1, num - (text.Length + 1));
							MessageBox.Show("0.0 sized pack file detected: " + item2 + "\nResubscribe to the file and try again. \nThe URL is: https://steamcommunity.com/sharedfiles/filedetails/?id=" + text3);
							try
							{
								File.Delete(item2);
							}
							catch
							{
							}
							break;
						}
						string text4 = tww2DataPath + "\\" + text2;
						if (File.Exists(text4) && new FileInfo(text4).Length == 0L)
						{
							try
							{
								File.Delete(text4);
							}
							catch
							{
							}
						}
						if (File.Exists(text4))
						{
							DateTime lastWriteTime2 = File.GetLastWriteTime(text4);
							if (!(lastWriteTime2 > lastWriteTime) && !(lastWriteTime2 == lastWriteTime))
							{
								try
								{
									File.Delete(text4);
								}
								catch
								{
									MessageBox.Show("Error trying to delete: " + text4);
								}
								CopyFile(item2, text4);
								flag = true;
							}
						}
						else
						{
							CopyFile(item2, text4);
							flag = true;
						}
					}
				}
				if (!flag)
				{
					continue;
				}
				string text5 = "";
				SteamData steamData = new SteamData();
				foreach (string item3 in list3)
				{
					int num2 = item3.LastIndexOf("\\");
					string displayFileName = item3.Substring(num2 + 1);
					if (item3.EndsWith(".pack"))
					{
						text5 = item3;
						DateTime lastWriteTime3 = File.GetLastWriteTime(item3);
						steamData.HakLastUpdated = lastWriteTime3;
						steamData.DisplayFileName = displayFileName;
						steamData.HakFileName = item3;
					}
					else if (item3.EndsWith(".png"))
					{
						DateTime lastWriteTime4 = File.GetLastWriteTime(item3);
						steamData.PNGLaastUpdated = lastWriteTime4;
						steamData.PNGFileName = item3;
					}
				}
				if (text5 != "")
				{
					int num3 = text5.LastIndexOf("\\");
					string text6 = text5.Substring(num3 + 1);
					steamDictionary.Remove(text6);
					int num4 = text5.LastIndexOf(CoreData.appIDString);
					string text7 = text5.Substring(num4 + 1 + CoreData.appIDString.Length);
					string s = text7.Substring(0, text7.Length - text6.Length - 1);
					steamData.SteamID = ulong.Parse(s);
					SteamUGCDetails_t details = Workshop.GetDetails(steamData.SteamID);
					CSteamID cSteamID = new CSteamID(details.m_ulSteamIDOwner);
					SteamFriends.RequestUserInformation(cSteamID, true);
					string friendPersonaName = SteamFriends.GetFriendPersonaName(cSteamID);
					steamData.SteamTitle = details.m_rgchTitle;
					steamData.SteamAuthor = friendPersonaName;
					steamData.DisplayFileName = Path.GetFileName(steamData.HakFileName);
					steamData.SteamDescription = details.m_rgchDescription;
					steamData.SteamTags = details.m_rgchTags;
					steamData.SteamURL = details.m_rgchURL;
					if ((steamData.SteamLastUploadDate = FromUnixTime(details.m_rtimeUpdated)) < dateTime)
					{
						steamData.Outdated = true;
					}
					steamData.SteamURL = "https://steamcommunity.com/sharedfiles/filedetails/?id=" + steamData.SteamID;
					list2.Add(steamData);
					if (!steamDictionary.ContainsKey(steamData.DisplayFileName))
					{
						steamDictionary.Add(steamData.DisplayFileName, steamData);
					}
				}
				flag = false;
			}
			list2.Clear();
			list2 = null;
			SteamAPI.Shutdown();
			File.Delete(Application.StartupPath + "\\steam_appid.txt");
		}

		private void LoadPackFiles()
		{
			HandleContentFolder();
			string[] files = Directory.GetFiles(tww2DataPath, "*.pack");
			int num2 = files.Length;
			int vanillaCount = 0;
			int modCount = 0;
			XmlElement documentElement = new XmlDocument().DocumentElement;
			_vfxPacks = new ConcurrentBag<Pack>();
			new List<PackType>();
			ConcurrentBag<Pack> _modPacks = new ConcurrentBag<Pack>();
			ConcurrentBag<string> _LoadedMods = new ConcurrentBag<string>();
			Parallel.ForEach(files, (string file) =>
			{
				if ((File.GetAttributes(file) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
				{
					File.SetAttributes(file, FileAttributes.Normal);
				}
				PackType packType = GetPackType(file);
				bool flag = IsPackVanilla(file, packType);
				if (flag)
				{
					vanillaCount++;
				}
				else
				{
					modCount++;
				}
				if (!flag)
				{
					PackFileCodec packFileCodec = new PackFileCodec();
					PackFile packFile = null;
					List<string> list = new List<string>();
					try
					{
						packFile = packFileCodec.Open(file);
						// PERF FIX: Files/FileCount rebuild the whole file list on every call (O(n^2)); fetch once.
						List<PackedFile> allFiles = packFile.Files;
						list.Capacity = allFiles.Count;
						for (int i = 0; i < allFiles.Count; i++)
						{
							list.Add(allFiles[i].FullPath);
						}
					}
					catch
					{
						return;
					}
					if (backupMods && corePrefs.pathBackup != null)
					{
						string text = corePrefs.pathBackup + Path.GetFileName(file);
						if (File.Exists(text))
						{
							DateTime lastWriteTime = File.GetLastWriteTime(text);
							if (File.GetLastWriteTime(file) > lastWriteTime)
							{
								new FileInfo(file).CopyTo(text, true);
								FileInfo fileInfo = new FileInfo(file.Replace(".pack", ".png"));
								if (fileInfo.Exists)
								{
									fileInfo.CopyTo(text.Replace(".pack", ".png"), true);
								}
							}
						}
						else
						{
							new FileInfo(file).CopyTo(text, true);
							FileInfo fileInfo2 = new FileInfo(file.Replace(".pack", ".png"));
							if (fileInfo2.Exists)
							{
								fileInfo2.CopyTo(text.Replace(".pack", ".png"), true);
							}
						}
					}
					list.Where((string c) => c.Contains("readme.txt", StringComparison.OrdinalIgnoreCase)).ToList();
					Pack pack = new Pack(file, packType, flag, list);
					_modPacks.Add(pack);
					if (!_LoadedMods.Contains(pack.Name))
					{
						_LoadedMods.Add(pack.Name);
					}
					if (packType == PackType.Movie && !flag)
					{
						_vfxPacks.Add(pack);
					}
				}
			});
			modPacks = _modPacks.ToList();
			LoadedMods = _LoadedMods.ToList();
			LoadedMods.Sort();
			int num = 0;
			foreach (Pack modPack in modPacks)
			{
				if (!modPack.IsVanilla)
				{
					num += modPack.FileList.Count;
				}
			}
			if (!bypassConflictDetect)
			{
				foreach (Pack modPack2 in modPacks)
				{
					if (modPack2.IsVanilla)
					{
						continue;
					}
					foreach (Pack modPack3 in modPacks)
					{
						if (modPack3.IsVanilla || !(modPack3.Name != modPack2.Name))
						{
							continue;
						}
						foreach (string file in modPack2.FileList)
						{
							if (file != "extra_packfile.rpfm_reserved" && file != "settings.rpfm_reserved" && file != "notes.rpfm_reserved" && modPack3.FileList.Contains(file))
							{
								Conflict item = new Conflict(modPack3.Name, file);
								modPack2.Conflicts.Add(item);
							}
						}
					}
				}
			}
			ListAllMods();
			ColourListViewConflicts();
			LoadProfiles();
		}

		private void LoadPackFilesFromContent()
		{
			HandleContentFolderWithoutCopy();
			string[] files = Directory.GetFiles(tww2DataPath, "*.pack");
			int num2 = files.Length;
			int vanillaCount = 0;
			int modCount = 0;
			XmlElement documentElement = new XmlDocument().DocumentElement;
			_vfxPacks = new ConcurrentBag<Pack>();
			new List<PackType>();
			ConcurrentBag<Pack> _modPacks = new ConcurrentBag<Pack>();
			ConcurrentBag<string> _LoadedMods = new ConcurrentBag<string>();
			Parallel.ForEach(files, (string file) =>
			{
				if ((File.GetAttributes(file) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
				{
					File.SetAttributes(file, FileAttributes.Normal);
				}
				PackType packType = GetPackType(file);
				bool flag = IsPackVanilla(file, packType);
				if (flag)
				{
					vanillaCount++;
				}
				else
				{
					modCount++;
				}
				if (!flag)
				{
					PackFileCodec packFileCodec = new PackFileCodec();
					PackFile packFile = null;
					List<string> list = new List<string>();
					try
					{
						packFile = packFileCodec.Open(file);
						// PERF FIX: Files/FileCount rebuild the whole file list on every call (O(n^2)); fetch once.
						List<PackedFile> allFiles = packFile.Files;
						list.Capacity = allFiles.Count;
						for (int i = 0; i < allFiles.Count; i++)
						{
							list.Add(allFiles[i].FullPath);
						}
					}
					catch
					{
						return;
					}
					if (backupMods && corePrefs.pathBackup != null)
					{
						string text = corePrefs.pathBackup + Path.GetFileName(file);
						if (File.Exists(text))
						{
							DateTime lastWriteTime = File.GetLastWriteTime(text);
							if (File.GetLastWriteTime(file) > lastWriteTime)
							{
								new FileInfo(file).CopyTo(text, true);
								FileInfo fileInfo = new FileInfo(file.Replace(".pack", ".png"));
								if (fileInfo.Exists)
								{
									fileInfo.CopyTo(text.Replace(".pack", ".png"), true);
								}
							}
						}
						else
						{
							new FileInfo(file).CopyTo(text, true);
							FileInfo fileInfo2 = new FileInfo(file.Replace(".pack", ".png"));
							if (fileInfo2.Exists)
							{
								fileInfo2.CopyTo(text.Replace(".pack", ".png"), true);
							}
						}
					}
					list.Where((string c) => c.Contains("readme.txt", StringComparison.OrdinalIgnoreCase)).ToList();
					Pack pack = new Pack(file, packType, flag, list);
					_modPacks.Add(pack);
					if (!_LoadedMods.Contains(pack.Name))
					{
						_LoadedMods.Add(pack.Name);
					}
					if (packType == PackType.Movie && !flag)
					{
						_vfxPacks.Add(pack);
					}
				}
			});
			modPacks = _modPacks.ToList();
			LoadedMods = _LoadedMods.ToList();
			LoadedMods.Sort();
			int num = 0;
			foreach (Pack modPack in modPacks)
			{
				if (!modPack.IsVanilla)
				{
					num += modPack.FileList.Count;
				}
			}
			if (!bypassConflictDetect)
			{
				foreach (Pack modPack2 in modPacks)
				{
					if (modPack2.IsVanilla)
					{
						continue;
					}
					foreach (Pack modPack3 in modPacks)
					{
						if (modPack3.IsVanilla || !(modPack3.Name != modPack2.Name))
						{
							continue;
						}
						foreach (string file in modPack2.FileList)
						{
							if (file != "extra_packfile.rpfm_reserved" && file != "settings.rpfm_reserved" && file != "notes.rpfm_reserved" && modPack3.FileList.Contains(file))
							{
								Conflict item = new Conflict(modPack3.Name, file);
								modPack2.Conflicts.Add(item);
							}
						}
					}
				}
			}
			ListAllMods();
			ColourListViewConflicts();
			LoadProfiles();
		}

		public void UpdateManagerWar2()
		{
			Encoding encoding = new UTF8Encoding(false);
			using (StreamWriter streamWriter = new StreamWriter(Application.StartupPath + "\\steam_appid.txt", false, encoding))
			{
				streamWriter.WriteLine(CoreData.appIDString);
				streamWriter.Close();
			}
			if (!SteamAPIWrapper.Init())
			{
				MessageBox.Show("Steam must be running in order for this feature to work. The manager will now exit. Please start Steam and then start the manager.");
				Application.Exit();
				Environment.Exit(0);
			}
			AppId_t nConsumerAppId = default(AppId_t);
			nConsumerAppId.m_AppId = 594570u;
			PublishedFileId_t nPublishedFileID = new PublishedFileId_t(1399787504uL);
			UGCUpdateHandle_t handle = SteamUGC.StartItemUpdate(nConsumerAppId, nPublishedFileID);
			string pchChangeNote = "4-17-2023 update of the Mod Manager on Steam!";
			SteamUGC.SetItemTitle(handle, "Kaedrin's Mod Manager v1.9.0");
			SteamUGC.SetItemPreview(handle, "D:\\Uploads\\TotalWarModManager.jpg");
			SteamUGC.SetItemContent(handle, "D:\\Uploads\\TotalWarModManager.zip");
			SteamUGC.SubmitItemUpdate(handle, pchChangeNote);
			Workshop.GetDetails(nPublishedFileID.m_PublishedFileId);
			SteamAPI.Shutdown();
			File.Delete(Application.StartupPath + "\\steam_appid.txt");
		}

		public void UpdateManagerWar3()
		{
			Encoding encoding = new UTF8Encoding(false);
			using (StreamWriter streamWriter = new StreamWriter(Application.StartupPath + "\\steam_appid.txt", false, encoding))
			{
				streamWriter.WriteLine(CoreData.appIDString);
				streamWriter.Close();
			}
			if (!SteamAPIWrapper.Init())
			{
				MessageBox.Show("Steam must be running in order for this feature to work. The manager will now exit. Please start Steam and then start the manager.");
				Application.Exit();
				Environment.Exit(0);
			}
			AppId_t nConsumerAppId = default(AppId_t);
			nConsumerAppId.m_AppId = 1142710u;
			PublishedFileId_t nPublishedFileID = new PublishedFileId_t(1399787504uL);
			UGCUpdateHandle_t handle = SteamUGC.StartItemUpdate(nConsumerAppId, nPublishedFileID);
			string pchChangeNote = "4-17-2023 update of the Mod Manager on Steam!";
			SteamUGC.SetItemTitle(handle, "Kaedrin's Mod Manager v1.9.0");
			SteamUGC.SetItemPreview(handle, "D:\\Uploads\\TotalWarModManager.jpg");
			SteamUGC.SetItemContent(handle, "D:\\Uploads\\TotalWarModManager.zip");
			SteamUGC.SubmitItemUpdate(handle, pchChangeNote);
			Workshop.GetDetails(nPublishedFileID.m_PublishedFileId);
			SteamAPI.Shutdown();
			File.Delete(Application.StartupPath + "\\steam_appid.txt");
		}

		public void UpdateTestProj()
		{
			Encoding encoding = new UTF8Encoding(false);
			using (StreamWriter streamWriter = new StreamWriter(Application.StartupPath + "\\steam_appid.txt", false, encoding))
			{
				streamWriter.WriteLine(CoreData.appIDString);
				streamWriter.Close();
			}
			if (!SteamAPIWrapper.Init())
			{
				MessageBox.Show("Steam must be running in order for this feature to work. The manager will now exit. Please start Steam and then start the manager.");
				Application.Exit();
				Environment.Exit(0);
			}
			AppId_t nConsumerAppId = default(AppId_t);
			nConsumerAppId.m_AppId = 594570u;
			PublishedFileId_t publishedFileId_t = new PublishedFileId_t(1389013797uL);
			PublishedFileId_t nChildPublishedFileID = new PublishedFileId_t(1456828999uL);
			UGCUpdateHandle_t handle = SteamUGC.StartItemUpdate(nConsumerAppId, publishedFileId_t);
			string pchChangeNote = "Updated to the no intro movie pack version to test if the .zip is screwing things up.";
			SteamUGC.SetItemTitle(handle, "DO NOT SUBSCRIBE IF YOU SEE THIS - Title Test: !kaed_test.pack");
			SteamUGC.SetItemPreview(handle, "D:\\Downloads\\TWW2\\v1.7\\!kaed_test.PNG");
			SteamUGC.SetItemContent(handle, "D:\\Downloads\\TWW2\\v1.7\\!kaed_test.pack");
			SteamUGC.AddDependency(publishedFileId_t, nChildPublishedFileID);
			SteamUGC.SubmitItemUpdate(handle, pchChangeNote);
			Workshop.GetDetails(publishedFileId_t.m_PublishedFileId);
			SteamAPI.Shutdown();
			File.Delete(Application.StartupPath + "\\steam_appid.txt");
		}

		public void UpdateMovie()
		{
			Encoding encoding = new UTF8Encoding(false);
			using (StreamWriter streamWriter = new StreamWriter(Application.StartupPath + "\\steam_appid.txt", false, encoding))
			{
				streamWriter.WriteLine(CoreData.appIDString);
				streamWriter.Close();
			}
			if (!SteamAPIWrapper.Init())
			{
				MessageBox.Show("Steam must be running in order for this feature to work. The manager will now exit. Please start Steam and then start the manager.");
				Application.Exit();
				Environment.Exit(0);
			}
			AppId_t nConsumerAppId = default(AppId_t);
			nConsumerAppId.m_AppId = 594570u;
			PublishedFileId_t nPublishedFileID = new PublishedFileId_t(1401657794uL);
			Workshop.GetDetails(nPublishedFileID.m_PublishedFileId);
			UGCUpdateHandle_t uGCUpdateHandle_t = SteamUGC.StartItemUpdate(nConsumerAppId, nPublishedFileID);
			string pchChangeNote = "Movie Test";
			SteamUGC.SetItemTitle(uGCUpdateHandle_t, "Movie Test");
			SteamUGC.SetItemPreview(uGCUpdateHandle_t, "D:\\\\Uploads\\WarhammerModManager.PNG");
			SteamUGC.SetItemContent(uGCUpdateHandle_t, "D:\\\\Uploads\\BeamRemoval_KTest.pack");
			SteamUGC.SetItemUpdateLanguage(uGCUpdateHandle_t, "english");
			SteamUGC.SetItemTags(uGCUpdateHandle_t, new List<string> { "Ui", "Mod" });
			SteamUGC.SubmitItemUpdate(uGCUpdateHandle_t, pchChangeNote);
			Workshop.GetDetails(nPublishedFileID.m_PublishedFileId);
			SteamAPI.Shutdown();
			File.Delete(Application.StartupPath + "\\steam_appid.txt");
		}

		private void FormMain_Shown(object sender, EventArgs e)
		{
			UpdateLabel();
			if (labelOrderInfo.Left + labelOrderInfo.Width > Width)
			{
				Width = labelOrderInfo.Left + labelOrderInfo.Width + 5;
			}
			if (labelDataPath.Top + labelDataPath.Height > Height)
			{
				Height = labelDataPath.Top + labelDataPath.Height + 5;
			}
			CheckScriptDirectory();
			AskAboutPath();
			if (!Directory.Exists(tww2DataPath))
			{
				launchGameButton.Enabled = false;
				MessageBox.Show("Unable to locate data directory at game path.\n\n" + tww2DataPath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			if (corePrefs.dataLoadingMethod == 0)
			{
				LoadPackFiles();
			}
			else if (corePrefs.dataLoadingMethod == 1)
			{
				LoadPackFilesFromContent();
			}
			else if (corePrefs.dataLoadingMethod == 2)
			{
				LoadPackFiles();
			}
			listViewMod.Sort();
			lvwColumnSorter.Order = SortOrder.None;
		}

		private void AskAboutPath()
		{
			if (warhammer3Game.Checked)
			{
				CoreData.SetGame(warhammer3Game.Name);
				xmlDataFileName = CoreData.xmlDataFileName;
				lastChecked = warhammer3Game.Name;
			}
			else if (warhammer2Game.Checked)
			{
				CoreData.SetGame(warhammer2Game.Name);
				xmlDataFileName = CoreData.xmlDataFileName;
				lastChecked = warhammer2Game.Name;
			}
			else if (warhammerGame.Checked)
			{
				CoreData.SetGame(warhammerGame.Name);
				xmlDataFileName = CoreData.xmlDataFileName;
				lastChecked = warhammerGame.Name;
			}
			else if (gameAttila.Checked)
			{
				CoreData.SetGame(gameAttila.Name);
				xmlDataFileName = CoreData.xmlDataFileName;
				lastChecked = gameAttila.Name;
			}
			else if (gameEmpire.Checked)
			{
				CoreData.SetGame(gameEmpire.Name);
				xmlDataFileName = CoreData.xmlDataFileName;
				lastChecked = gameEmpire.Name;
			}
			else if (gameShogun2.Checked)
			{
				CoreData.SetGame(gameShogun2.Name);
				xmlDataFileName = CoreData.xmlDataFileName;
				lastChecked = gameShogun2.Name;
			}
			else if (gameNapoleon.Checked)
			{
				CoreData.SetGame(gameNapoleon.Name);
				xmlDataFileName = CoreData.xmlDataFileName;
				lastChecked = gameNapoleon.Name;
			}
			else if (gameRome2.Checked)
			{
				CoreData.SetGame(gameRome2.Name);
				xmlDataFileName = CoreData.xmlDataFileName;
				lastChecked = gameRome2.Name;
			}
			else if (threeKingdomsGame.Checked)
			{
				CoreData.SetGame(threeKingdomsGame.Name);
				xmlDataFileName = CoreData.xmlDataFileName;
				lastChecked = threeKingdomsGame.Name;
			}
			else if (troyGame.Checked)
			{
				CoreData.SetGame(troyGame.Name);
				xmlDataFileName = CoreData.xmlDataFileName;
				lastChecked = troyGame.Name;
			}
			else if (throbGame.Checked)
			{
				CoreData.SetGame(throbGame.Name);
				xmlDataFileName = CoreData.xmlDataFileName;
				lastChecked = throbGame.Name;
			}
			bool flag = false;
			bool flag2 = false;
			tww2Path = CoreData.exePath;
			if (string.IsNullOrEmpty(tww2Path) || !Directory.Exists(tww2Path))
			{
				tww2Path = CoreData.GetGameDirectory();
				if ((string.IsNullOrEmpty(tww2Path) || !Directory.Exists(tww2Path)) && (string.IsNullOrEmpty(tww2Path) || !Directory.Exists(tww2Path)))
				{
					findW2TWPathFolderBrowserDialog.Description = "Unable to automatically find the path to " + throbGame.Text;
				}
				if (string.IsNullOrEmpty(tww2Path))
				{
					warhammer2Game.Enabled = false;
				}
				else
				{
					flag2 = true;
				}
			}
			else
			{
				flag2 = true;
			}
			if (flag | flag2)
			{
				if (!flag2 & flag)
				{
					warhammer2Game.Checked = false;
					warhammerGame.Checked = true;
					tww2DataPath = Path.Combine(tww2Path, "data");
					labelDataPath.Text = tww2DataPath;
				}
				else
				{
					tww2DataPath = Path.Combine(tww2Path, "data");
					labelDataPath.Text = tww2DataPath;
				}
				CoreData.dataPath = tww2DataPath;
				CoreData.exePath = tww2Path;
				exePath = tww2Path;
				if (!Directory.Exists(CoreData.profilePath))
				{
					Directory.CreateDirectory(CoreData.profilePath);
				}
			}
			else
			{
				MessageBox.Show("Mod Manager can't work without the path to at least one Total War game , exiting!", "Unable to continue", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				Environment.Exit(0);
			}
		}

		private void CheckScriptDirectory()
		{
			if (!Directory.Exists(CoreData.myAppDataPath))
			{
				Directory.CreateDirectory(CoreData.myAppDataPath);
			}
			if (!Directory.Exists(CoreData.profilePath))
			{
				Directory.CreateDirectory(CoreData.profilePath);
			}
		}

		private PackType GetPackType(string path)
		{
			int result = 0;
			try
			{
				using (FileStream input = File.OpenRead(path))
				{
					using (BinaryReader binaryReader = new BinaryReader(input))
					{
						binaryReader.ReadChars(4);
						result = binaryReader.ReadInt32();
					}
				}
			}
			catch
			{
				MessageBox.Show("Critical Error encountered! The following pack file has an invalid pack file type which prevent mods from displaying/loading correctly. Error getting the type of pack file for: " + path);
			}
			return (PackType)result;
		}

		private bool IsPackVanilla(string file, PackType packType)
		{
			if (warhammer3Game.Checked || warhammer2Game.Checked || warhammerGame.Checked || threeKingdomsGame.Checked || troyGame.Checked)
			{
				if (packType == PackType.Mod || packType == PackType.Movie)
				{
					return false;
				}
				return true;
			}
			if (packType == PackType.Mod)
			{
				return false;
			}
			return true;
		}

		private PackHeader ReadHeader(BinaryReader reader)
		{
			PackHeader packHeader = new PackHeader();
			new string(reader.ReadChars(4));
			int num = reader.ReadInt32();
			bool flag = false;
			foreach (PackType value in Enum.GetValues(typeof(PackType)))
			{
				if (num == (int)value)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				throw new InvalidDataException("Unknown pack type " + num);
			}
			packHeader.PackType = (PackType)num;
			int num2 = reader.ReadInt32();
			int num3 = reader.ReadInt32();
			reader.BaseStream.Seek(16L, SeekOrigin.Begin);
			packHeader.FileCount = reader.ReadUInt32();
			uint num4 = reader.ReadUInt32();
			packHeader.DataStart = 28 + num4;
			reader.ReadUInt32();
			reader.BaseStream.Seek(28L, SeekOrigin.Begin);
			for (int i = 0; i < num2; i++)
			{
				ReadZeroTerminatedAscii(reader);
			}
			packHeader.DataStart += num3;
			return packHeader;
		}

		private static string ReadZeroTerminatedAscii(BinaryReader reader)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (byte b = reader.ReadByte(); b != 0; b = reader.ReadByte())
			{
				stringBuilder.Append((char)b);
			}
			return stringBuilder.ToString();
		}

		private void informationToolStripMenuItem_Click(object sender, EventArgs e)
		{
		}

		private void listViewMod_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (listViewMod.SelectedItems.Count > 0)
			{
				moveUpButton.Enabled = true;
				moveDownButton.Enabled = true;
				ListViewItem listViewItem = listViewMod.SelectedItems[0];
				for (int i = 0; i < modPacks.Count; i++)
				{
					if (!(modPacks[i].Name == listViewItem.Text))
					{
						continue;
					}
					string filePath = modPacks[i].FilePath;
					filePath = filePath.Replace(".pack", ".png");
					if (File.Exists(filePath))
					{
						if (pictureBox1.Image != null)
						{
							pictureBox1.Image.Dispose();
						}
						Image image = null;
						try
						{
							using (Bitmap original = new Bitmap(filePath))
							{
								image = new Bitmap(original);
							}
						}
						catch
						{
							MessageBox.Show(filePath + " is not a valid image. This is typically because a .WEBP image has been renamed as a .PNG file instead of using the export function.");
						}
						pictureBox1.Image = image;
					}
					else
					{
						pictureBox1.Image = Resources.yield_sign;
					}
					break;
				}
			}
			else
			{
				moveUpButton.Enabled = false;
				moveDownButton.Enabled = false;
			}
		}

		private void profilesComboBox_Click(object sender, EventArgs e)
		{
			listViewMod.SelectedIndices.Clear();
		}

		private void Resubscribe_OnItemDownloaded(object sender, Workshop.DownloadItemEventArgs e)
		{
			EItemState downloadStatus = Workshop.GetDownloadStatus(e.Result.m_nPublishedFileId.m_PublishedFileId);
			EItemState eItemState = EItemState.k_EItemStateSubscribed | EItemState.k_EItemStateInstalled;
			if (downloadStatus == eItemState)
			{
				Interlocked.Decrement(ref downloadCount);
			}
			string pchFolder = "";
			SteamApps.GetAppInstallDir(e.Result.m_unAppID, out pchFolder, 300u);
			pchFolder = pchFolder.Replace("common\\Total War WARHAMMER II", "workshop\\content\\594570");
			pchFolder = pchFolder + "\\" + e.Result.m_nPublishedFileId.ToString();
			if (Directory.Exists(pchFolder))
			{
				Directory.GetFiles(pchFolder).Count();
				int num = 0;
			}
		}

		public static DateTime FromUnixTime(long unixTime)
		{
			return epoch.AddSeconds(unixTime);
		}

		private void OnSteamUGCQueryCompleted(SteamUGCQueryCompleted_t pCallback, bool bIOFailure)
		{
		}

		private void ListAllMods()
		{
			int num = 0;
			int num2 = 4;
			bool flag = false;
			steamWorkshopIntegrationBETAToolStripMenuItem.Checked = true;
			if (steamWorkshopIntegrationBETAToolStripMenuItem.Checked)
			{
				flag = true;
				num2 = 6;
				ColumnHeader columnHeader = new ColumnHeader();
				columnHeader.Text = "Author";
				ColumnHeader columnHeader2 = new ColumnHeader();
				columnHeader2.Text = "Title";
				ColumnHeader columnHeader3 = new ColumnHeader();
				columnHeader3.Text = "Type";
				ColumnHeader columnHeader4 = new ColumnHeader();
				columnHeader4.Text = "Conflicts";
				if (bypassConflictDetect)
				{
					num2--;
					if (listViewMod.Columns.Count < 5)
					{
						listViewMod.Columns.RemoveAt(1);
						listViewMod.Columns.Add(columnHeader2);
						listViewMod.Columns.Add(columnHeader3);
						listViewMod.Columns[0].Text = "File Name";
						listViewMod.Columns.Add(columnHeader);
						listViewMod.Columns[1].Width = 100;
						listViewMod.Columns[2].Width = 400;
						listViewMod.Columns[3].Width = 50;
						listViewMod.Columns[4].Width = 200;
					}
					else if (listViewMod.Columns.Count == 6)
					{
						listViewMod.Columns.RemoveAt(1);
					}
				}
				else
				{
					if (listViewMod.Columns.Count == 5 && listViewMod.Columns[1].Text != "Conflicts")
					{
						listViewMod.Columns.Insert(1, columnHeader4);
					}
					if (listViewMod.Columns.Count == 3)
					{
						listViewMod.Columns.Add(columnHeader2);
					}
					if (listViewMod.Columns.Count == 4)
					{
						listViewMod.Columns.Add(columnHeader3);
					}
					if (listViewMod.Columns.Count == 5)
					{
						listViewMod.Columns[0].Text = "File Name";
					}
					if (listViewMod.Columns.Count == 5)
					{
						listViewMod.Columns.Add(columnHeader);
					}
					listViewMod.Columns[1].Width = 100;
					listViewMod.Columns[2].Width = 100;
					listViewMod.Columns[3].Width = 400;
					listViewMod.Columns[4].Width = 50;
					listViewMod.Columns[5].Width = 200;
				}
			}
			int num3 = 0;
			bool flag2 = false;
			foreach (Pack modPack in modPacks)
			{
				num3 = 0;
				flag2 = false;
				if (modPack.IsVanilla)
				{
					continue;
				}
				string[] array = new string[num2];
				array[num3] = modPack.Name;
				num3++;
				if (!bypassConflictDetect)
				{
					if (modPack.Conflicts.Count > 0)
					{
						array[num3] = modPack.Conflicts.Count + " conflicts";
					}
					else
					{
						array[num3] = "None";
					}
					num3++;
				}
				DateTime lastWriteTime = File.GetLastWriteTime(modPack.FilePath);
				string text = lastWriteTime.ToString("d", CultureInfo.CurrentCulture);
				bool flag3 = (DateTime.Now - lastWriteTime).TotalDays < 7.0;
				array[num3] = text;
				num3++;
				if (flag)
				{
					SteamData value = new SteamData();
					if (steamDictionary.TryGetValue(modPack.Name, out value))
					{
						array[num3] = value.SteamTitle;
						num3++;
						foreach (Pack vfxPack in _vfxPacks)
						{
							if (vfxPack.Name == modPack.Name)
							{
								array[num3] = "Movie";
								num++;
								break;
							}
						}
						if (array[num3] == null)
						{
							array[num3] = "Pack";
						}
						num3++;
						array[num3] = value.SteamAuthor;
						flag2 = value.Outdated;
					}
					else
					{
						foreach (Pack vfxPack2 in _vfxPacks)
						{
							if (vfxPack2.Name == modPack.Name)
							{
								array[num3] = "Movie";
								num++;
								break;
							}
						}
						if (array[num3] == null && num2 == 5)
						{
							if (num3 == 2)
							{
								array[num3] = array[0];
								array[num3 + 1] = "Pack";
							}
							else
							{
								array[num3] = "Pack";
							}
						}
					}
				}
				else if (listViewMod.Columns.Count == 5)
				{
					listViewMod.Columns.RemoveAt(3);
					listViewMod.Columns.RemoveAt(3);
					listViewMod.Columns[2].Width = 430;
				}
				ListViewItem listViewItem = new ListViewItem(array);
				listViewItem.UseItemStyleForSubItems = false;
				int num4 = 2;
				if (bypassConflictDetect)
				{
					num4--;
				}
				SteamData updInfo;
				if (flag && steamDictionary != null && steamDictionary.TryGetValue(modPack.Name, out updInfo) && updInfo.UpdateAvailable)
				{
					listViewItem.SubItems[num4].Tag = UpdateDotTag;
				}
				if (highlightRecentMods && flag3)
				{
					listViewItem.SubItems[num4].BackColor = Color.LightBlue;
					if (currentTheme == ColorScheme.dark)
					{
						listViewItem.SubItems[num4].BackColor = Color.FromArgb(0, 85, 129, 50);
					}
				}
				if (flag2)
				{
					listViewItem.SubItems[num4].BackColor = Color.Orange;
					if (currentTheme == ColorScheme.dark)
					{
						listViewItem.SubItems[num4].BackColor = Color.FromArgb(0, 201, 88, 12);
					}
				}
				listViewMod.Items.Add(listViewItem);
			}
			labelEnabledMovies.Text = "Movies Enabled: " + num;
			if (num == 0)
			{
				labelEnabledMovies.Visible = false;
			}
			else
			{
				labelEnabledMovies.Visible = true;
			}
		}

		private void ColourListViewConflicts()
		{
			if (bypassConflictDetect)
			{
				return;
			}
			foreach (ListViewItem item in listViewMod.Items)
			{
				if (item.SubItems[1].Text != "None")
				{
					ListViewItem.ListViewSubItem listViewSubItem = item.SubItems[1];
					listViewSubItem.Font = new Font(listViewMod.Font, FontStyle.Underline);
					listViewSubItem.ForeColor = Color.FromKnownColor(KnownColor.HotTrack);
				}
			}
		}

		private void launchGameButton_Click(object sender, EventArgs e)
		{
			if (lvwColumnSorter.SortColumn != 0)
			{
				if (MessageBox.Show("Warning, you have a column other than the file name sorted. You should sort the file name alphabetically and then launch the game. Choose Cancel to abort the launch and sort the files or click OK launch the game regardless.", "Invalid Sort Detected", MessageBoxButtons.OKCancel) != DialogResult.OK)
				{
					return;
				}
				LaunchGame();
			}
			if (lvwColumnSorter.SortColumn == 0 && lvwColumnSorter.Order == SortOrder.Descending)
			{
				if (MessageBox.Show("Warning, you have the file name column sorted in descending order. You should sort the file name alphabetically and then launch the game. Choose Cancel to abort the launch and sort the files or click OK launch the game regardless.", "Invalid Sort Detected", MessageBoxButtons.OKCancel) != DialogResult.OK)
				{
					return;
				}
				LaunchGame();
			}
			LaunchGame();
		}

		private void LaunchGame()
		{
			Path.Combine(tww2Path, CoreData.exeName);
			WriteUserScript();
			WriteProfile("LastUsedMods");
			string fileName = Path.Combine(CoreData.exePath, CoreData.exeName);
			if (bypassCALauncher)
			{
				Encoding encoding = new UTF8Encoding(false);
				using (StreamWriter streamWriter = new StreamWriter(Application.StartupPath + "\\steam_appid.txt", false, encoding))
				{
					streamWriter.WriteLine(CoreData.appIDString);
					streamWriter.Close();
				}
				if (!SteamAPIWrapper.Init())
				{
					MessageBox.Show("Steam must be running in order for this feature to work. The manager will now exit. Please start Steam and then start the manager.");
					Application.Exit();
					Environment.Exit(0);
				}
			}
			Process process = new Process();
			process.StartInfo.Arguments = "";
			process.StartInfo.FileName = fileName;
			process.StartInfo.WorkingDirectory = tww2Path;
			process.Start();
		}

		private void WriteUserScript()
		{
			List<string> list = ListActivatedMods();
			if (!Directory.Exists(CoreData.scriptPath))
			{
				MessageBox.Show("Please run the game at least once before trying to use KMM. The required Creative Assembly directories have not been created yet! The mod manager will now exit.");
				if (bypassCALauncher)
				{
					SteamAPI.Shutdown();
					try
					{
						if (File.Exists(Application.StartupPath + "\\steam_appid.txt"))
						{
							File.Delete(Application.StartupPath + "\\steam_appid.txt");
						}
					}
					catch
					{
					}
					Environment.Exit(0);
				}
				list.Clear();
			}
			if (list.Count > 0)
			{
				Encoding encoding = new UTF8Encoding(false);
				using (StreamWriter streamWriter = new StreamWriter(CoreData.userScriptFilepath, false, encoding))
				{
					foreach (string item in list)
					{
						streamWriter.WriteLine("mod \"" + item + "\";");
					}
					streamWriter.Close();
					return;
				}
			}
			if (File.Exists(CoreData.userScriptFilepath))
			{
				File.Delete(CoreData.userScriptFilepath);
			}
		}

		private void WriteSettings()
		{
			if (File.Exists(CoreData.settingsFilepath))
			{
				File.Delete(CoreData.settingsFilepath);
			}
			XmlDocument xmlDocument = new XmlDocument();
			XmlNode newChild = xmlDocument.CreateXmlDeclaration("1.0", "UTF-8", null);
			xmlDocument.AppendChild(newChild);
			XmlNode xmlNode = xmlDocument.CreateElement("Settings");
			XmlAttribute xmlAttribute = xmlDocument.CreateAttribute("AppId");
			xmlAttribute.Value = CoreData.appIDString;
			xmlNode.Attributes.Append(xmlAttribute);
			XmlNode xmlNode2 = xmlDocument.CreateElement("CheckedGameId");
			xmlNode2.InnerText = CoreData.appIDString;
			xmlNode.AppendChild(xmlNode2);
			XmlNode xmlNode3 = xmlDocument.CreateElement("CopyContentToData");
			xmlNode3.InnerText = copyContentToDataToolStripMenuItem.Checked.ToString();
			xmlNode.AppendChild(xmlNode3);
			XmlNode xmlNode4 = xmlDocument.CreateElement("BypassConflictDetection");
			xmlNode4.InnerText = bypassConflictDetectionToolStripMenuItem.Checked.ToString().ToLower();
			xmlNode.AppendChild(xmlNode4);
			XmlNode xmlNode5 = xmlDocument.CreateElement("EnableDarkMode");
			xmlNode5.InnerText = enableDarkModeToolStripMenuItem.Checked.ToString().ToLower();
			xmlNode.AppendChild(xmlNode5);
			XmlNode xmlNode6 = xmlDocument.CreateElement("EnableForceSteamDownloads");
			xmlNode6.InnerText = enableForceSteamDownloadsToolStripMenuItem.Checked.ToString().ToLower();
			xmlNode.AppendChild(xmlNode6);
			XmlNode xmlNode7 = xmlDocument.CreateElement("EnableDeleteWhileForcingSteamDownloads");
			xmlNode7.InnerText = enableDeleteWhileForcingSteamDownloadsToolStripMenuItem.Checked.ToString().ToLower();
			xmlNode.AppendChild(xmlNode7);
			XmlNode xmlNode8 = xmlDocument.CreateElement("BackupMods");
			xmlNode8.InnerText = backupModsToolStripMenuItem.Checked.ToString();
			xmlNode.AppendChild(xmlNode8);
			XmlNode newChild2 = xmlDocument.CreateElement("ConflictScanSelectedOnly");
			xmlNode.AppendChild(newChild2);
			XmlNode xmlNode9 = xmlDocument.CreateElement("PFM_Path");
			xmlNode9.InnerText = corePrefs.pathPFM;
			xmlNode.AppendChild(xmlNode9);
			XmlNode xmlNode10 = xmlDocument.CreateElement("RPFM_Path");
			xmlNode10.InnerText = corePrefs.pathRPFM;
			xmlNode.AppendChild(xmlNode10);
			XmlNode xmlNode11 = xmlDocument.CreateElement("Backup_Path");
			xmlNode11.InnerText = corePrefs.pathBackup;
			xmlNode.AppendChild(xmlNode11);
			XmlNode xmlNode12 = xmlDocument.CreateElement("UseSteam");
			xmlNode12.InnerText = steamWorkshopIntegrationBETAToolStripMenuItem.Checked.ToString();
			xmlNode.AppendChild(xmlNode12);
			XmlNode xmlNode13 = xmlDocument.CreateElement("UseLastProfile");
			xmlNode13.InnerText = useLastProfileMenuItem.Checked.ToString();
			xmlNode.AppendChild(xmlNode13);
			XmlNode xmlNode14 = xmlDocument.CreateElement("HighlightRecentMods");
			xmlNode14.InnerText = highlightRecentMods.ToString().ToLower();
			xmlNode.AppendChild(xmlNode14);
			XmlNode xmlNode15 = xmlDocument.CreateElement("ProfileName");
			if (profilesComboBox.SelectedItem != null)
			{
				xmlNode15.InnerText = profilesComboBox.SelectedItem.ToString();
			}
			else
			{
				xmlNode15.InnerText = "";
			}
			xmlNode.AppendChild(xmlNode15);
			XmlNode xmlNode16 = xmlDocument.CreateElement("GameList");
			XmlNode xmlNode17 = xmlDocument.CreateElement("Game");
			xmlNode17.InnerText = CoreData.appDataGameName;
			XmlAttribute xmlAttribute2 = xmlDocument.CreateAttribute("ExePath");
			xmlAttribute2.Value = tww2Path;
			xmlNode17.Attributes.Append(xmlAttribute2);
			xmlAttribute2 = xmlDocument.CreateAttribute("AppId");
			xmlAttribute2.Value = CoreData.appIDString;
			xmlNode17.Attributes.Append(xmlAttribute2);
			xmlAttribute2 = xmlDocument.CreateAttribute("ExeName");
			xmlAttribute2.Value = CoreData.appDataGameName;
			xmlNode17.Attributes.Append(xmlAttribute2);
			xmlNode16.AppendChild(xmlNode17);
			xmlNode.AppendChild(xmlNode16);
			xmlDocument.AppendChild(xmlNode);
			xmlDocument.Save(CoreData.settingsFilepath);
		}

		private void LoadSettings()
		{
			if (File.Exists(CoreData.settingsFilepath))
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(CoreData.settingsFilepath);
				foreach (XmlNode childNode in xmlDocument.DocumentElement.ChildNodes)
				{
					if (childNode.Name == "CheckedGameId")
					{
						ToolStripMenuItem toolStripMenuItem = null;
						if (childNode.InnerText == "1142710" && warhammer3Game.Enabled)
						{
							toolStripMenuItem = warhammer3Game;
						}
						if (childNode.InnerText == "594570" && warhammer2Game.Enabled)
						{
							toolStripMenuItem = warhammer2Game;
						}
						if (childNode.InnerText == "364360" && warhammerGame.Enabled)
						{
							toolStripMenuItem = warhammerGame;
						}
						if (childNode.InnerText == "712100" && throbGame.Enabled)
						{
							toolStripMenuItem = throbGame;
						}
						if (childNode.InnerText == "325610" && gameAttila.Enabled)
						{
							toolStripMenuItem = gameAttila;
						}
						if (childNode.InnerText == "10500" && gameEmpire.Enabled)
						{
							toolStripMenuItem = gameEmpire;
						}
						if (childNode.InnerText == "214950" && gameRome2.Enabled)
						{
							toolStripMenuItem = gameRome2;
						}
						if (childNode.InnerText == "34330" && gameShogun2.Enabled)
						{
							toolStripMenuItem = gameShogun2;
						}
						if (childNode.InnerText == "34030" && gameNapoleon.Enabled)
						{
							toolStripMenuItem = gameNapoleon;
						}
						if (childNode.InnerText == "779340" && threeKingdomsGame.Enabled)
						{
							toolStripMenuItem = threeKingdomsGame;
						}
						if (childNode.InnerText == "1099410" && troyGame.Enabled)
						{
							toolStripMenuItem = troyGame;
						}
						if (toolStripMenuItem != null)
						{
							foreach (ToolStripMenuItem dropDownItem in gameToolStripMenuItem.DropDownItems)
							{
								if (dropDownItem.Name != toolStripMenuItem.Name)
								{
									dropDownItem.Checked = false;
								}
								else
								{
									dropDownItem.Checked = true;
								}
							}
							CoreData.SetGame(toolStripMenuItem.Name);
						}
						else
						{
							foreach (ToolStripMenuItem dropDownItem2 in gameToolStripMenuItem.DropDownItems)
							{
								if (dropDownItem2.Enabled)
								{
									dropDownItem2.Checked = true;
									CoreData.SetGame(dropDownItem2.Name);
									break;
								}
							}
						}
					}
					if (childNode.Name == "UseSteam")
					{
						steamWorkshopIntegrationBETAToolStripMenuItem.Checked = true;
					}
					if (childNode.Name == "UseLastProfile")
					{
						try
						{
							useLastProfileMenuItem.Checked = bool.Parse(childNode.InnerText);
							useLastProfile = useLastProfileMenuItem.Checked;
						}
						catch (Exception)
						{
						}
					}
					if (childNode.Name == "HighlightRecentMods")
					{
						try
						{
							highlightRecentModsMenuItem.Checked = bool.Parse(childNode.InnerText);
							highlightRecentMods = highlightRecentModsMenuItem.Checked;
						}
						catch (Exception)
						{
						}
					}
					if (childNode.Name == "BackupMods")
					{
						try
						{
							backupModsToolStripMenuItem.Checked = bool.Parse(childNode.InnerText);
							backupMods = backupModsToolStripMenuItem.Checked;
						}
						catch (Exception)
						{
						}
					}
					if (childNode.Name == "CopyContentToData")
					{
						try
						{
							copyContentToDataToolStripMenuItem.Checked = bool.Parse(childNode.InnerText);
							copyContentToData = copyContentToDataToolStripMenuItem.Checked;
						}
						catch (Exception)
						{
						}
					}
					if (childNode.Name == "BypassConflictDetection")
					{
						try
						{
							bypassConflictDetectionToolStripMenuItem.Checked = bool.Parse(childNode.InnerText);
							bypassConflictDetect = bypassConflictDetectionToolStripMenuItem.Checked;
							if (bypassConflictDetect)
							{
								lvwColumnSorter.DateColumn = 1;
							}
						}
						catch (Exception)
						{
						}
					}
					if (childNode.Name == "EnableDarkMode")
					{
						try
						{
							enableDarkModeToolStripMenuItem.Checked = bool.Parse(childNode.InnerText);
							darkMode = enableDarkModeToolStripMenuItem.Checked;
							if (darkMode)
							{
								currentTheme = ColorScheme.dark;
								ChangeTheme(currentTheme, Controls);
							}
							else
							{
								currentTheme = ColorScheme.light;
							}
						}
						catch (Exception)
						{
						}
					}
					if (childNode.Name == "EnableForceSteamDownloads")
					{
						try
						{
							enableForceSteamDownloadsToolStripMenuItem.Checked = bool.Parse(childNode.InnerText);
							forceSteamDownloads = enableForceSteamDownloadsToolStripMenuItem.Checked;
						}
						catch (Exception)
						{
						}
					}
					if (childNode.Name == "EnableDeleteWhileForcingSteamDownloads")
					{
						try
						{
							enableDeleteWhileForcingSteamDownloadsToolStripMenuItem.Checked = bool.Parse(childNode.InnerText);
							deleteWhileForcingSteamDownloads = enableDeleteWhileForcingSteamDownloadsToolStripMenuItem.Checked;
						}
						catch (Exception)
						{
						}
					}
					if (childNode.Name == "ProfileName")
					{
						profileNameUsed = childNode.InnerText;
					}
					if (childNode.Name == "Backup_Path")
					{
						corePrefs.pathBackup = childNode.InnerText;
						if (corePrefs.pathBackup == "")
						{
							corePrefs.pathBackup = null;
						}
					}
					if (childNode.Name == "RPFM_Path")
					{
						corePrefs.pathRPFM = childNode.InnerText;
						if (corePrefs.pathRPFM == "")
						{
							corePrefs.pathRPFM = null;
						}
					}
					if (childNode.Name == "PFM_Path")
					{
						corePrefs.pathPFM = childNode.InnerText;
						if (corePrefs.pathPFM == "")
						{
							corePrefs.pathPFM = null;
						}
					}
					bool flag = childNode.Name == "ConflictScanSelectedOnly";
					corePrefs.dataLoadingMethod = 1;
				}
				tww2DataPath = CoreData.dataPath;
			}
			else
			{
				MessageBox.Show("User preferences reset, new options available. Dark mode will take effect the next time you start KMM unless you disable this new option.", "New menu options available");
			}
		}

		private string getHtml(string url)
		{
			string result = string.Empty;
			try
			{
				HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
				httpWebRequest.Method = "GET";
				HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				result = new StreamReader(httpWebResponse.GetResponseStream()).ReadToEnd();
				httpWebResponse.Close();
			}
			catch
			{
			}
			return result;
		}

		private void launchGameToolStripMenuItem_Click(object sender, EventArgs e)
		{
			LaunchGame();
		}

		private void labelDataPath_DoubleClick(object sender, EventArgs e)
		{
			while (findW2TWPathFolderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				if (File.Exists(Path.Combine(findW2TWPathFolderBrowserDialog.SelectedPath, "Warhammer2.exe")))
				{
					tww2Path = findW2TWPathFolderBrowserDialog.SelectedPath;
					tww2DataPath = Path.Combine(tww2Path, "data");
					WriteSettings();
					break;
				}
				MessageBox.Show("Warhammer2.exe not found in that directory, please try again.", "Invalid directory", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		private void selectGameDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
		{
			findW2TWPathFolderBrowserDialog.SelectedPath = tww2Path;
			while (findW2TWPathFolderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				if (File.Exists(Path.Combine(findW2TWPathFolderBrowserDialog.SelectedPath, "Warhammer2.exe")))
				{
					tww2Path = findW2TWPathFolderBrowserDialog.SelectedPath;
					tww2DataPath = Path.Combine(tww2Path, "data");
					WriteSettings();
					break;
				}
				MessageBox.Show("Warhammer2.exe not found in that directory, please try again.", "Invalid directory", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		private void FormMain_Resize(object sender, EventArgs e)
		{
		}

		private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing)
			{
				WriteSettings();
			}
			if (File.Exists(CoreData.userScriptFilepath))
			{
				File.Delete(CoreData.userScriptFilepath);
			}
			if (bypassCALauncher)
			{
				SteamAPI.Shutdown();
				try
				{
					if (File.Exists(Application.StartupPath + "\\steam_appid.txt"))
					{
						File.Delete(Application.StartupPath + "\\steam_appid.txt");
					}
				}
				catch
				{
				}
			}
			GC.Collect();
		}

		private void SaveUserSessionData(Dictionary<string, SteamData> stmDictionary)
		{
			using (FileStream stream = new FileStream("UserSessionLookupDictionarySerialized2.xml", FileMode.Create))
			{
				using (StreamWriter w = new StreamWriter(stream))
				{
					using (XmlTextWriter xmlTextWriter = new XmlTextWriter(w))
					{
						xmlTextWriter.Formatting = Formatting.Indented;
						xmlTextWriter.Indentation = 4;
						foreach (SteamData value in steamDictionary.Values)
						{
							SteamData steamDatum = value;
						}
					}
				}
			}
		}

		private void steamWorkshopIntegrationBETAToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SteamWorkshopIntegration = !SteamWorkshopIntegration;
			bool steamWorkshopIntegration = SteamWorkshopIntegration;
			RefreshApp();
		}

		private void RefreshApp()
		{
			if (enableAllCheckBox.Checked)
			{
				beingReset = true;
				enableAllCheckBox.Checked = false;
				beingReset = false;
			}
			lvwColumnSorter.Order = SortOrder.Ascending;
			modPacks.Clear();
			listViewMod.BeginUpdate();
			listViewMod.Items.Clear();
			LoadPackFiles();
			listViewMod.EndUpdate();
			if (pictureBox1.Image != null)
			{
				pictureBox1.Image.Dispose();
				pictureBox1.Image = null;
			}
		}

		private void refreshManagerButton_Click(object sender, EventArgs e)
		{
			if (Process.GetProcessesByName("StringExtensions").Any())
			{
				MessageBox.Show("Please close the game first.");
			}
			RefreshApp();
		}

		private void setGetFileData(string CheckBoxName)
		{
			CoreData.SetGame(CheckBoxName);
			CoreData.UpdateGameDirectory();
			AskAboutPath();
			tww2Path = CoreData.exePath;
			tww2DataPath = CoreData.dataPath;
			exeName = CoreData.exeName;
			RefreshApp();
		}

		private void refreshMenuItem_Click(object sender, EventArgs e)
		{
			RefreshApp();
		}

		private List<Key> zGetSubkeysValue(string path, RegistryHive hive)
		{
			List<Key> list = new List<Key>();
			using (RegistryKey registryKey = RegistryKey.OpenBaseKey(hive, RegistryView.Default))
			{
				using (RegistryKey registryKey2 = registryKey.OpenSubKey(path))
				{
					string[] subKeyNames = registryKey2.GetSubKeyNames();
					foreach (string keyName in subKeyNames)
					{
						Key item = zGetKeyValue(registryKey, keyName);
						list.Add(item);
					}
					return list;
				}
			}
		}

		private Key zGetKeyValue(RegistryKey hive, string keyName)
		{
			Key key = new Key
			{
				KeyName = keyName
			};
			using (RegistryKey registryKey = hive.OpenSubKey(keyName))
			{
				string[] valueNames = registryKey.GetValueNames();
				foreach (string text in valueNames)
				{
					object value = registryKey.GetValue(text);
					KeyValuePair<string, object> item = new KeyValuePair<string, object>(text, value);
					key.Values.Add(item);
				}
				return key;
			}
		}

		private void cleanDataButton_Click(object sender, EventArgs e)
		{
			CleanData();
		}

		public void CleanData()
		{
			DialogResult dialogResult = MessageBox.Show("Are you sure you want to remove all mod files from the Data folder? Any mods that have not been updated since mid-May of 2018 will have to be re-downloaded using the CA Launcher (at this time).", "Cleaning up the Data folder, one dirty mod at a time...", MessageBoxButtons.YesNo);
			if (dialogResult == DialogResult.Yes)
			{
				foreach (Pack modPack in modPacks)
				{
					if (!modPack.IsVanilla)
					{
						string path = modPack.FilePath.Replace(".pack", ".png");
						try
						{
							File.Delete(modPack.FilePath);
							File.Delete(path);
						}
						catch
						{
						}
					}
				}
				modPacks.Clear();
				listViewMod.Items.Clear();
				labelModCountNum.Text = "0";
			}
			else
			{
				int num = 7;
			}
		}

		private void mergeSelectedModsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				PackFileCodec packFileCodec = new PackFileCodec();
				PackFile packFile = null;
				foreach (ListViewItem checkedItem in listViewMod.CheckedItems)
				{
					if (checkedItem == null)
					{
						continue;
					}
					for (int i = 0; i < modPacks.Count; i++)
					{
						if (!(modPacks[i].Name == checkedItem.Text))
						{
							continue;
						}
						if (packFile == null)
						{
							string text = modPacks[i].FilePath.Replace(".pack", "_merge.pack");
							File.Copy(modPacks[i].FilePath, text, true);
							packFile = packFileCodec.Open(text);
							break;
						}
						foreach (PackedFile file in new PackFileCodec().Open(modPacks[i].FilePath).Files)
						{
							packFile.Add(file);
						}
						break;
					}
				}
				if (packFile != null)
				{
					packFileCodec.Save(packFile);
				}
			}
			catch
			{
			}
		}

		private void FormMain_Load(object sender, EventArgs e)
		{
		}

		private void unsubscribeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!File.Exists(CoreData.appIdPath))
			{
				Encoding encoding = new UTF8Encoding(false);
				using (StreamWriter streamWriter = new StreamWriter(CoreData.appIdPath, false, encoding))
				{
					streamWriter.WriteLine(CoreData.appIDString);
					streamWriter.Close();
				}
			}
			if (!SteamAPIWrapper.Init())
			{
				MessageBox.Show("Steam must be running in order for this feature to work. The manager will now exit. Please start Steam and then start the manager.");
				Application.Exit();
				Environment.Exit(0);
			}
			try
			{
				ListViewItem listViewItem = listViewMod.SelectedItems[0];
				if (listViewItem == null)
				{
					return;
				}
				int num = -1;
				for (int i = 0; i < modPacks.Count; i++)
				{
					if (modPacks[i].Name == listViewItem.Text)
					{
						num = i;
						break;
					}
				}
				if (num > -1)
				{
					SteamData value = null;
					steamDictionary.TryGetValue(modPacks[num].Name, out value);
					PublishedFileId_t nPublishedFileID = default(PublishedFileId_t);
					nPublishedFileID.m_PublishedFileId = value.SteamID;
					SteamUGC.UnsubscribeItem(nPublishedFileID);
					SteamAPIWrapper.RunCallbacks();
					string path = modPacks[num].FilePath.Replace(".pack", ".png");
					File.Delete(modPacks[num].FilePath);
					if (File.Exists(path))
					{
						File.Delete(path);
					}
					int length = tww2Path.IndexOf("common");
					Directory.Delete(string.Concat(string.Concat(tww2Path.Substring(0, length) + "workshop\\content\\" + CoreData.appIDString, "\\"), value.SteamID.ToString()), true);
					RefreshApp();
				}
			}
			catch
			{
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Warhammer2MM.FormMain));
			this.deleteProfileButton = new System.Windows.Forms.Button();
			this.saveProfileButton = new System.Windows.Forms.Button();
			this.lbl_profiles = new System.Windows.Forms.Label();
			this.profilesComboBox = new System.Windows.Forms.ComboBox();
			this.moveDownButton = new System.Windows.Forms.Button();
			this.moveUpButton = new System.Windows.Forms.Button();
			this.launchGameButton = new System.Windows.Forms.Button();
			this.labelMod = new System.Windows.Forms.Label();
			this.groupBoxProfiles = new System.Windows.Forms.GroupBox();
			this.labelOrderInfo = new System.Windows.Forms.Label();
			this.findW2TWPathFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
			this.menuStrip = new System.Windows.Forms.MenuStrip();
			this.functionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.launchGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.refreshMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.revertToVanillaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.cleanDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.cleanDataAndExitApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.selectGameDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.steamWorkshopIntegrationBETAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.useLastProfileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.highlightRecentModsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyContentToDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.remberLastGUILayoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.backupModsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.chooseBackupLocationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.choosePackFileManagerLocationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.chooseRustysPackFileManagerLocationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.detectGameInstallationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.bypassCALauncherBetaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.bypassConflictDetectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.enableDarkModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.enableForceSteamDownloadsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.enableDeleteWhileForcingSteamDownloadsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			this.openContentFolderInFileExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openDataFolderInFileExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.profilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exportProfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.importProfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exportProfileToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exportProfileWithSteamXMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exportAllModsWithSteamURLsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exportCheckedModsToHTMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.informationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutWarhammerModManager = new System.Windows.Forms.ToolStripMenuItem();
			this.contactToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.dataPath = new System.Windows.Forms.ToolStripMenuItem();
			this.readmeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.gameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.warhammer3Game = new System.Windows.Forms.ToolStripMenuItem();
			this.warhammer2Game = new System.Windows.Forms.ToolStripMenuItem();
			this.warhammerGame = new System.Windows.Forms.ToolStripMenuItem();
			this.gameAttila = new System.Windows.Forms.ToolStripMenuItem();
			this.gameRome2 = new System.Windows.Forms.ToolStripMenuItem();
			this.gameEmpire = new System.Windows.Forms.ToolStripMenuItem();
			this.gameNapoleon = new System.Windows.Forms.ToolStripMenuItem();
			this.gameShogun2 = new System.Windows.Forms.ToolStripMenuItem();
			this.throbGame = new System.Windows.Forms.ToolStripMenuItem();
			this.threeKingdomsGame = new System.Windows.Forms.ToolStripMenuItem();
			this.troyGame = new System.Windows.Forms.ToolStripMenuItem();
			this.userScriptExtensionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.expandedDebugSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.labelDataPath = new System.Windows.Forms.Label();
			this.BumpPriority = new System.Windows.Forms.CheckBox();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.cleanDataButton = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.refreshManagerButton = new System.Windows.Forms.Button();
			this.nameColumn = new System.Windows.Forms.ColumnHeader();
			this.conflictColumn = new System.Windows.Forms.ColumnHeader();
			this.DateColumn = new System.Windows.Forms.ColumnHeader();
			this.listViewMod = new System.Windows.Forms.ListView();
			this.label2 = new System.Windows.Forms.Label();
			this.enableAllCheckBox = new System.Windows.Forms.CheckBox();
			this.importProfile = new System.Windows.Forms.OpenFileDialog();
			this.exportProfile = new System.Windows.Forms.SaveFileDialog();
			this.labelModCount = new System.Windows.Forms.Label();
			this.labelModCountNum = new System.Windows.Forms.Label();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.showContentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openSteamPage2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openSteamPage3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openWithPFMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openWithRPFMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.setUserNote2StripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.SteamStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openSteamPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.voteUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.downloadModAgainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.resubscribeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.updateModforModAuthorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.uploadModToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.saveToArchiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.restoreFromArchiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.versionDatedXToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.versionDatedYToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.versionDatedZToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.unsubscribeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.unsubscribeMultipleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.deleteDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteDataMultipleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteContentStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
			this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
			this.addModToExistingProfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.setReadOnlyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.hideModFromProfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mergeSelectedModsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openPackFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.label1 = new System.Windows.Forms.Label();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.label3 = new System.Windows.Forms.Label();
			this.labelEnabledMovies = new System.Windows.Forms.Label();
			this.groupBoxProfiles.SuspendLayout();
			this.menuStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
			this.contextMenuStrip1.SuspendLayout();
			base.SuspendLayout();
			this.deleteProfileButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			this.deleteProfileButton.Location = new System.Drawing.Point(100, 70);
			this.deleteProfileButton.Name = "deleteProfileButton";
			this.deleteProfileButton.Size = new System.Drawing.Size(70, 23);
			this.deleteProfileButton.TabIndex = 19;
			this.deleteProfileButton.Text = "&Delete";
			this.deleteProfileButton.UseVisualStyleBackColor = true;
			this.deleteProfileButton.Click += new System.EventHandler(this.deleteProfileButton_Click);
			this.saveProfileButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			this.saveProfileButton.Location = new System.Drawing.Point(24, 70);
			this.saveProfileButton.Name = "saveProfileButton";
			this.saveProfileButton.Size = new System.Drawing.Size(70, 23);
			this.saveProfileButton.TabIndex = 18;
			this.saveProfileButton.Text = "&Save";
			this.saveProfileButton.UseVisualStyleBackColor = true;
			this.saveProfileButton.Click += new System.EventHandler(this.saveProfileButton_Click);
			this.lbl_profiles.AutoSize = true;
			this.lbl_profiles.Font = new System.Drawing.Font("Verdana", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			this.lbl_profiles.Location = new System.Drawing.Point(21, 24);
			this.lbl_profiles.Name = "lbl_profiles";
			this.lbl_profiles.Size = new System.Drawing.Size(87, 13);
			this.lbl_profiles.TabIndex = 16;
			this.lbl_profiles.Text = "Select &Profile:";
			this.lbl_profiles.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.profilesComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			this.profilesComboBox.FormattingEnabled = true;
			this.profilesComboBox.Location = new System.Drawing.Point(24, 40);
			this.profilesComboBox.Name = "profilesComboBox";
			this.profilesComboBox.Size = new System.Drawing.Size(175, 24);
			this.profilesComboBox.TabIndex = 17;
			this.profilesComboBox.SelectedIndexChanged += new System.EventHandler(this.profilesComboBox_SelectedIndexChanged);
			this.profilesComboBox.Click += new System.EventHandler(this.profilesComboBox_Click);
			this.moveDownButton.Enabled = false;
			this.moveDownButton.Font = new System.Drawing.Font("Wingdings", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			this.moveDownButton.Location = new System.Drawing.Point(157, 73);
			this.moveDownButton.Name = "moveDownButton";
			this.moveDownButton.Size = new System.Drawing.Size(29, 27);
			this.moveDownButton.TabIndex = 14;
			this.moveDownButton.Text = "ê";
			this.moveDownButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.moveDownButton.UseCompatibleTextRendering = true;
			this.moveDownButton.UseMnemonic = false;
			this.moveDownButton.UseVisualStyleBackColor = true;
			this.moveDownButton.Click += new System.EventHandler(this.moveDownButton_Click);
			this.moveUpButton.Enabled = false;
			this.moveUpButton.Font = new System.Drawing.Font("Wingdings", 16f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			this.moveUpButton.Location = new System.Drawing.Point(122, 73);
			this.moveUpButton.Name = "moveUpButton";
			this.moveUpButton.Size = new System.Drawing.Size(29, 27);
			this.moveUpButton.TabIndex = 13;
			this.moveUpButton.Text = "é";
			this.moveUpButton.UseCompatibleTextRendering = true;
			this.moveUpButton.UseMnemonic = false;
			this.moveUpButton.UseVisualStyleBackColor = true;
			this.moveUpButton.Click += new System.EventHandler(this.moveUpButton_Click);
			this.launchGameButton.Font = new System.Drawing.Font("Verdana", 18f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.launchGameButton.Image = (System.Drawing.Image)resources.GetObject("launchGameButton.Image");
			this.launchGameButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.launchGameButton.Location = new System.Drawing.Point(1057, 704);
			this.launchGameButton.Name = "launchGameButton";
			this.launchGameButton.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
			this.launchGameButton.Size = new System.Drawing.Size(204, 81);
			this.launchGameButton.TabIndex = 15;
			this.launchGameButton.Text = "Launch";
			this.launchGameButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.launchGameButton.UseVisualStyleBackColor = true;
			this.launchGameButton.Click += new System.EventHandler(this.launchGameButton_Click);
			this.labelMod.Font = new System.Drawing.Font("Verdana", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.labelMod.Location = new System.Drawing.Point(12, 81);
			this.labelMod.Name = "labelMod";
			this.labelMod.Size = new System.Drawing.Size(107, 22);
			this.labelMod.TabIndex = 22;
			this.labelMod.Text = "Mod Packs";
			this.labelMod.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.groupBoxProfiles.Controls.Add(this.deleteProfileButton);
			this.groupBoxProfiles.Controls.Add(this.profilesComboBox);
			this.groupBoxProfiles.Controls.Add(this.lbl_profiles);
			this.groupBoxProfiles.Controls.Add(this.saveProfileButton);
			this.groupBoxProfiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.groupBoxProfiles.Location = new System.Drawing.Point(1057, 496);
			this.groupBoxProfiles.Name = "groupBoxProfiles";
			this.groupBoxProfiles.Size = new System.Drawing.Size(215, 114);
			this.groupBoxProfiles.TabIndex = 24;
			this.groupBoxProfiles.TabStop = false;
			this.groupBoxProfiles.Text = "Mod Profiles";
			this.labelOrderInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelOrderInfo.Font = new System.Drawing.Font("Verdana", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			this.labelOrderInfo.Location = new System.Drawing.Point(462, 36);
			this.labelOrderInfo.Name = "labelOrderInfo";
			this.labelOrderInfo.Size = new System.Drawing.Size(822, 37);
			this.labelOrderInfo.TabIndex = 25;
			this.labelOrderInfo.Text = "DO NOT CHANGE MOD PRIORITY without reading the Mod Troubleshooting Guide first. Moving a checked mod higher up will mean its contents will load with priority over any mods below it. ";
			this.labelOrderInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.findW2TWPathFolderBrowserDialog.Description = "Unable to automatically find the path to Warhammer2.exe, please locate it manually.";
			this.findW2TWPathFolderBrowserDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
			this.findW2TWPathFolderBrowserDialog.ShowNewFolderButton = false;
			this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[6] { this.functionsToolStripMenuItem, this.optionsToolStripMenuItem, this.profilesToolStripMenuItem, this.informationToolStripMenuItem, this.gameToolStripMenuItem, this.userScriptExtensionsToolStripMenuItem });
			this.menuStrip.Location = new System.Drawing.Point(0, 0);
			this.menuStrip.Name = "menuStrip";
			this.menuStrip.Size = new System.Drawing.Size(1284, 24);
			this.menuStrip.TabIndex = 26;
			this.menuStrip.Text = "menuStrip1";
			this.functionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[9] { this.launchGameToolStripMenuItem, this.refreshMenuItem, this.revertToVanillaToolStripMenuItem, this.cleanDataToolStripMenuItem, this.cleanDataAndExitApplicationToolStripMenuItem, this.toolStripMenuItem1, this.selectGameDirectoryToolStripMenuItem, this.toolStripMenuItem2, this.exitToolStripMenuItem });
			this.functionsToolStripMenuItem.Name = "functionsToolStripMenuItem";
			this.functionsToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
			this.functionsToolStripMenuItem.Text = "&Tool";
			this.launchGameToolStripMenuItem.Name = "launchGameToolStripMenuItem";
			this.launchGameToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
			this.launchGameToolStripMenuItem.Text = "&Launch Game";
			this.launchGameToolStripMenuItem.Click += new System.EventHandler(this.launchGameToolStripMenuItem_Click);
			this.refreshMenuItem.Name = "refreshMenuItem";
			this.refreshMenuItem.Size = new System.Drawing.Size(240, 22);
			this.refreshMenuItem.Text = "Refresh";
			this.refreshMenuItem.Click += new System.EventHandler(this.refreshMenuItem_Click);
			this.revertToVanillaToolStripMenuItem.Name = "revertToVanillaToolStripMenuItem";
			this.revertToVanillaToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
			this.revertToVanillaToolStripMenuItem.Text = "&Reset Program Settings";
			this.revertToVanillaToolStripMenuItem.Click += new System.EventHandler(this.revertToVanillaToolStripMenuItem_Click);
			this.cleanDataToolStripMenuItem.Name = "cleanDataToolStripMenuItem";
			this.cleanDataToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
			this.cleanDataToolStripMenuItem.Text = "Clean Data";
			this.cleanDataToolStripMenuItem.Click += new System.EventHandler(this.cleanDataToolStripMenuItem_Click);
			this.cleanDataAndExitApplicationToolStripMenuItem.Name = "cleanDataAndExitApplicationToolStripMenuItem";
			this.cleanDataAndExitApplicationToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
			this.cleanDataAndExitApplicationToolStripMenuItem.Text = "Clean Data and Exit Application";
			this.cleanDataAndExitApplicationToolStripMenuItem.Click += new System.EventHandler(this.cleanDataAndExitApplicationToolStripMenuItem_Click);
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(237, 6);
			this.toolStripMenuItem1.Visible = false;
			this.selectGameDirectoryToolStripMenuItem.Enabled = false;
			this.selectGameDirectoryToolStripMenuItem.Name = "selectGameDirectoryToolStripMenuItem";
			this.selectGameDirectoryToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
			this.selectGameDirectoryToolStripMenuItem.Text = "Change Game Directory";
			this.selectGameDirectoryToolStripMenuItem.Visible = false;
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(237, 6);
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
			this.exitToolStripMenuItem.Text = "&Exit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[18]
			{
				this.steamWorkshopIntegrationBETAToolStripMenuItem, this.useLastProfileMenuItem, this.highlightRecentModsMenuItem, this.copyContentToDataToolStripMenuItem, this.remberLastGUILayoutToolStripMenuItem, this.backupModsToolStripMenuItem, this.chooseBackupLocationToolStripMenuItem, this.choosePackFileManagerLocationToolStripMenuItem, this.chooseRustysPackFileManagerLocationToolStripMenuItem, this.detectGameInstallationsToolStripMenuItem,
				this.bypassCALauncherBetaToolStripMenuItem, this.bypassConflictDetectionToolStripMenuItem, this.enableDarkModeToolStripMenuItem, this.enableForceSteamDownloadsToolStripMenuItem, this.enableDeleteWhileForcingSteamDownloadsToolStripMenuItem, this.toolStripSeparator6, this.openContentFolderInFileExplorerToolStripMenuItem, this.openDataFolderInFileExplorerToolStripMenuItem
			});
			this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
			this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
			this.optionsToolStripMenuItem.Text = "Options";
			this.steamWorkshopIntegrationBETAToolStripMenuItem.CheckOnClick = true;
			this.steamWorkshopIntegrationBETAToolStripMenuItem.Name = "steamWorkshopIntegrationBETAToolStripMenuItem";
			this.steamWorkshopIntegrationBETAToolStripMenuItem.Size = new System.Drawing.Size(326, 22);
			this.steamWorkshopIntegrationBETAToolStripMenuItem.Text = "Steam Workshop Integration [BETA]";
			this.steamWorkshopIntegrationBETAToolStripMenuItem.Visible = false;
			this.steamWorkshopIntegrationBETAToolStripMenuItem.Click += new System.EventHandler(this.steamWorkshopIntegrationBETAToolStripMenuItem_Click);
			this.useLastProfileMenuItem.Checked = true;
			this.useLastProfileMenuItem.CheckOnClick = true;
			this.useLastProfileMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.useLastProfileMenuItem.Name = "useLastProfileMenuItem";
			this.useLastProfileMenuItem.Size = new System.Drawing.Size(326, 22);
			this.useLastProfileMenuItem.Text = "Use Last Profile";
			this.useLastProfileMenuItem.Click += new System.EventHandler(this.useLastProfileToolStripMenuItem_Click);
			this.highlightRecentModsMenuItem.Checked = true;
			this.highlightRecentModsMenuItem.CheckOnClick = true;
			this.highlightRecentModsMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.highlightRecentModsMenuItem.Name = "highlightRecentModsMenuItem";
			this.highlightRecentModsMenuItem.Size = new System.Drawing.Size(326, 22);
			this.highlightRecentModsMenuItem.Text = "Highlight Recent Mods";
			this.highlightRecentModsMenuItem.ToolTipText = "Highlight mods updated within the last 7 days with a yellow background. This change will take effect the next time you click the refresh button or restart the application.";
			this.highlightRecentModsMenuItem.Click += new System.EventHandler(this.highlightRecentModsMenuItem_Click);
			this.copyContentToDataToolStripMenuItem.Checked = true;
			this.copyContentToDataToolStripMenuItem.CheckOnClick = true;
			this.copyContentToDataToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.copyContentToDataToolStripMenuItem.Name = "copyContentToDataToolStripMenuItem";
			this.copyContentToDataToolStripMenuItem.Size = new System.Drawing.Size(326, 22);
			this.copyContentToDataToolStripMenuItem.Text = "Copy Content to Data";
			this.copyContentToDataToolStripMenuItem.Visible = false;
			this.remberLastGUILayoutToolStripMenuItem.CheckOnClick = true;
			this.remberLastGUILayoutToolStripMenuItem.Enabled = false;
			this.remberLastGUILayoutToolStripMenuItem.Name = "remberLastGUILayoutToolStripMenuItem";
			this.remberLastGUILayoutToolStripMenuItem.Size = new System.Drawing.Size(326, 22);
			this.remberLastGUILayoutToolStripMenuItem.Text = "Remember Last GUI Layout";
			this.remberLastGUILayoutToolStripMenuItem.Visible = false;
			this.backupModsToolStripMenuItem.CheckOnClick = true;
			this.backupModsToolStripMenuItem.Name = "backupModsToolStripMenuItem";
			this.backupModsToolStripMenuItem.Size = new System.Drawing.Size(326, 22);
			this.backupModsToolStripMenuItem.Text = "Backup Mods";
			this.backupModsToolStripMenuItem.Click += new System.EventHandler(this.backupModsToolStripMenuItem_Click);
			this.chooseBackupLocationToolStripMenuItem.Name = "chooseBackupLocationToolStripMenuItem";
			this.chooseBackupLocationToolStripMenuItem.Size = new System.Drawing.Size(326, 22);
			this.chooseBackupLocationToolStripMenuItem.Text = "Choose Backup Location";
			this.chooseBackupLocationToolStripMenuItem.Click += new System.EventHandler(this.chooseBackupLocationToolStripMenuItem_Click);
			this.choosePackFileManagerLocationToolStripMenuItem.Name = "choosePackFileManagerLocationToolStripMenuItem";
			this.choosePackFileManagerLocationToolStripMenuItem.Size = new System.Drawing.Size(326, 22);
			this.choosePackFileManagerLocationToolStripMenuItem.Text = "Choose Pack File Manager Location";
			this.choosePackFileManagerLocationToolStripMenuItem.Click += new System.EventHandler(this.choosePackFileManagerLocationToolStripMenuItem_Click);
			this.chooseRustysPackFileManagerLocationToolStripMenuItem.Name = "chooseRustysPackFileManagerLocationToolStripMenuItem";
			this.chooseRustysPackFileManagerLocationToolStripMenuItem.Size = new System.Drawing.Size(326, 22);
			this.chooseRustysPackFileManagerLocationToolStripMenuItem.Text = "Choose Rusted Pack File Manager Location";
			this.chooseRustysPackFileManagerLocationToolStripMenuItem.Click += new System.EventHandler(this.chooseRustysPackFileManagerLocationToolStripMenuItem_Click);
			this.detectGameInstallationsToolStripMenuItem.Name = "detectGameInstallationsToolStripMenuItem";
			this.detectGameInstallationsToolStripMenuItem.Size = new System.Drawing.Size(326, 22);
			this.detectGameInstallationsToolStripMenuItem.Text = "Detect Game Installations";
			this.detectGameInstallationsToolStripMenuItem.Click += new System.EventHandler(this.detectGameInstallationsToolStripMenuItem_Click);
			this.bypassCALauncherBetaToolStripMenuItem.Checked = true;
			this.bypassCALauncherBetaToolStripMenuItem.CheckOnClick = true;
			this.bypassCALauncherBetaToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.bypassCALauncherBetaToolStripMenuItem.Name = "bypassCALauncherBetaToolStripMenuItem";
			this.bypassCALauncherBetaToolStripMenuItem.Size = new System.Drawing.Size(326, 22);
			this.bypassCALauncherBetaToolStripMenuItem.Text = "Bypass CA Launcher (Beta)";
			this.bypassCALauncherBetaToolStripMenuItem.Visible = false;
			this.bypassCALauncherBetaToolStripMenuItem.Click += new System.EventHandler(this.bypassCALauncherBetaToolStripMenuItem_Click);
			this.bypassConflictDetectionToolStripMenuItem.Checked = true;
			this.bypassConflictDetectionToolStripMenuItem.CheckOnClick = true;
			this.bypassConflictDetectionToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.bypassConflictDetectionToolStripMenuItem.Name = "bypassConflictDetectionToolStripMenuItem";
			this.bypassConflictDetectionToolStripMenuItem.Size = new System.Drawing.Size(326, 22);
			this.bypassConflictDetectionToolStripMenuItem.Text = "Bypass Conflict Detection (Beta)";
			this.bypassConflictDetectionToolStripMenuItem.Click += new System.EventHandler(this.bypassConflictDetectionToolStripMenuItem_Click);
			this.enableDarkModeToolStripMenuItem.Checked = true;
			this.enableDarkModeToolStripMenuItem.CheckOnClick = true;
			this.enableDarkModeToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.enableDarkModeToolStripMenuItem.Name = "enableDarkModeToolStripMenuItem";
			this.enableDarkModeToolStripMenuItem.Size = new System.Drawing.Size(326, 22);
			this.enableDarkModeToolStripMenuItem.Text = "Enable Dark Mode (restart required for changes)";
			this.enableDarkModeToolStripMenuItem.Click += new System.EventHandler(this.enableDarkModeToolStripMenuItem_Click);
			this.enableForceSteamDownloadsToolStripMenuItem.Checked = true;
			this.enableForceSteamDownloadsToolStripMenuItem.CheckOnClick = true;
			this.enableForceSteamDownloadsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.enableForceSteamDownloadsToolStripMenuItem.Name = "enableForceSteamDownloadsToolStripMenuItem";
			this.enableForceSteamDownloadsToolStripMenuItem.Size = new System.Drawing.Size(326, 22);
			this.enableForceSteamDownloadsToolStripMenuItem.Text = "Enable Force Steam Downloads";
			this.enableForceSteamDownloadsToolStripMenuItem.Click += new System.EventHandler(this.enableForceSteamDownloadsToolStripMenuItem_Click);
			this.enableDeleteWhileForcingSteamDownloadsToolStripMenuItem.CheckOnClick = true;
			this.enableDeleteWhileForcingSteamDownloadsToolStripMenuItem.Name = "enableDeleteWhileForcingSteamDownloadsToolStripMenuItem";
			this.enableDeleteWhileForcingSteamDownloadsToolStripMenuItem.Size = new System.Drawing.Size(326, 22);
			this.enableDeleteWhileForcingSteamDownloadsToolStripMenuItem.Text = "Enable Delete While Forcing Steam Downloads";
			this.enableDeleteWhileForcingSteamDownloadsToolStripMenuItem.Click += new System.EventHandler(this.enableDeleteWhileForcingSteamDownloadsToolStripMenuItem_Click);
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(323, 6);
			this.openContentFolderInFileExplorerToolStripMenuItem.Name = "openContentFolderInFileExplorerToolStripMenuItem";
			this.openContentFolderInFileExplorerToolStripMenuItem.Size = new System.Drawing.Size(326, 22);
			this.openContentFolderInFileExplorerToolStripMenuItem.Text = "Open Content Folder in File Explorer";
			this.openContentFolderInFileExplorerToolStripMenuItem.Click += new System.EventHandler(this.openContentFolderInFileExplorerToolStripMenuItem_Click);
			this.openDataFolderInFileExplorerToolStripMenuItem.Name = "openDataFolderInFileExplorerToolStripMenuItem";
			this.openDataFolderInFileExplorerToolStripMenuItem.Size = new System.Drawing.Size(326, 22);
			this.openDataFolderInFileExplorerToolStripMenuItem.Text = "Open Data Folder in File Explorer";
			this.openDataFolderInFileExplorerToolStripMenuItem.Click += new System.EventHandler(this.openDataFolderInFileExplorerToolStripMenuItem_Click);
			this.profilesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[6] { this.exportProfileToolStripMenuItem, this.importProfileToolStripMenuItem, this.exportProfileToClipboardToolStripMenuItem, this.exportProfileWithSteamXMLToolStripMenuItem, this.exportAllModsWithSteamURLsToolStripMenuItem, this.exportCheckedModsToHTMLToolStripMenuItem });
			this.profilesToolStripMenuItem.Name = "profilesToolStripMenuItem";
			this.profilesToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
			this.profilesToolStripMenuItem.Text = "Profiles";
			this.exportProfileToolStripMenuItem.Name = "exportProfileToolStripMenuItem";
			this.exportProfileToolStripMenuItem.Size = new System.Drawing.Size(257, 22);
			this.exportProfileToolStripMenuItem.Text = "Export Profile as Simple Text";
			this.exportProfileToolStripMenuItem.Click += new System.EventHandler(this.exportProfileToolStripMenuItem_Click);
			this.importProfileToolStripMenuItem.Name = "importProfileToolStripMenuItem";
			this.importProfileToolStripMenuItem.Size = new System.Drawing.Size(257, 22);
			this.importProfileToolStripMenuItem.Text = "Import Profile from Simple Text";
			this.importProfileToolStripMenuItem.Click += new System.EventHandler(this.importProfileToolStripMenuItem_Click);
			this.exportProfileToClipboardToolStripMenuItem.Name = "exportProfileToClipboardToolStripMenuItem";
			this.exportProfileToClipboardToolStripMenuItem.Size = new System.Drawing.Size(257, 22);
			this.exportProfileToClipboardToolStripMenuItem.Text = "Export Current Profile to Clipboard";
			this.exportProfileToClipboardToolStripMenuItem.Click += new System.EventHandler(this.exportProfileToClipboardToolStripMenuItem_Click);
			this.exportProfileWithSteamXMLToolStripMenuItem.Name = "exportProfileWithSteamXMLToolStripMenuItem";
			this.exportProfileWithSteamXMLToolStripMenuItem.Size = new System.Drawing.Size(257, 22);
			this.exportProfileWithSteamXMLToolStripMenuItem.Text = "Export Current Profile to HTML";
			this.exportProfileWithSteamXMLToolStripMenuItem.Click += new System.EventHandler(this.exportProfileWithSteamXMLToolStripMenuItem_Click);
			this.exportAllModsWithSteamURLsToolStripMenuItem.Name = "exportAllModsWithSteamURLsToolStripMenuItem";
			this.exportAllModsWithSteamURLsToolStripMenuItem.Size = new System.Drawing.Size(257, 22);
			this.exportAllModsWithSteamURLsToolStripMenuItem.Text = "Export All Mods to HTML";
			this.exportAllModsWithSteamURLsToolStripMenuItem.Click += new System.EventHandler(this.exportAllModsWithSteamURLsToolStripMenuItem_Click);
			this.exportCheckedModsToHTMLToolStripMenuItem.Name = "exportCheckedModsToHTMLToolStripMenuItem";
			this.exportCheckedModsToHTMLToolStripMenuItem.Size = new System.Drawing.Size(257, 22);
			this.exportCheckedModsToHTMLToolStripMenuItem.Text = "Export Checked Mods to HTML";
			this.exportCheckedModsToHTMLToolStripMenuItem.Visible = false;
			this.exportCheckedModsToHTMLToolStripMenuItem.Click += new System.EventHandler(this.exportCheckedModsToHTMLToolStripMenuItem_Click);
			this.informationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[4] { this.aboutWarhammerModManager, this.contactToolStripMenuItem, this.dataPath, this.readmeToolStripMenuItem });
			this.informationToolStripMenuItem.Name = "informationToolStripMenuItem";
			this.informationToolStripMenuItem.ShortcutKeyDisplayString = "";
			this.informationToolStripMenuItem.Size = new System.Drawing.Size(82, 20);
			this.informationToolStripMenuItem.Text = "&Information";
			this.informationToolStripMenuItem.Click += new System.EventHandler(this.informationToolStripMenuItem_Click);
			this.aboutWarhammerModManager.Name = "aboutWarhammerModManager";
			this.aboutWarhammerModManager.Size = new System.Drawing.Size(130, 22);
			this.aboutWarhammerModManager.Text = "About";
			this.aboutWarhammerModManager.Click += new System.EventHandler(this.aboutWarhammerModManager_Click);
			this.contactToolStripMenuItem.Name = "contactToolStripMenuItem";
			this.contactToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
			this.contactToolStripMenuItem.Text = "Contact";
			this.contactToolStripMenuItem.Visible = false;
			this.dataPath.Name = "dataPath";
			this.dataPath.Size = new System.Drawing.Size(130, 22);
			this.dataPath.Text = "Data Paths";
			this.dataPath.Click += new System.EventHandler(this.dataPath_Click);
			this.readmeToolStripMenuItem.Enabled = false;
			this.readmeToolStripMenuItem.Name = "readmeToolStripMenuItem";
			this.readmeToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
			this.readmeToolStripMenuItem.Text = "Readme";
			this.gameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[11]
			{
				this.warhammer3Game, this.warhammer2Game, this.warhammerGame, this.gameAttila, this.gameRome2, this.gameEmpire, this.gameNapoleon, this.gameShogun2, this.throbGame, this.threeKingdomsGame,
				this.troyGame
			});
			this.gameToolStripMenuItem.Name = "gameToolStripMenuItem";
			this.gameToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
			this.gameToolStripMenuItem.Text = "Game";
			this.warhammer3Game.CheckOnClick = true;
			this.warhammer3Game.Name = "warhammer3Game";
			this.warhammer3Game.Size = new System.Drawing.Size(180, 22);
			this.warhammer3Game.Text = "Warhammer 3";
			this.warhammer3Game.Click += new System.EventHandler(this.warhammer3Game_Click);
			this.warhammer2Game.CheckOnClick = true;
			this.warhammer2Game.Name = "warhammer2Game";
			this.warhammer2Game.Size = new System.Drawing.Size(180, 22);
			this.warhammer2Game.Text = "Warhammer 2";
			this.warhammer2Game.Click += new System.EventHandler(this.warhammer2Game_Click);
			this.warhammerGame.CheckOnClick = true;
			this.warhammerGame.Name = "warhammerGame";
			this.warhammerGame.Size = new System.Drawing.Size(180, 22);
			this.warhammerGame.Text = "Warhammer";
			this.warhammerGame.Click += new System.EventHandler(this.warhammerGame_Click);
			this.gameAttila.CheckOnClick = true;
			this.gameAttila.Name = "gameAttila";
			this.gameAttila.Size = new System.Drawing.Size(180, 22);
			this.gameAttila.Text = "Attila";
			this.gameAttila.Click += new System.EventHandler(this.gameAttila_Click);
			this.gameRome2.CheckOnClick = true;
			this.gameRome2.Name = "gameRome2";
			this.gameRome2.Size = new System.Drawing.Size(180, 22);
			this.gameRome2.Text = "Rome 2";
			this.gameRome2.Click += new System.EventHandler(this.gameRome2_Click);
			this.gameEmpire.CheckOnClick = true;
			this.gameEmpire.Name = "gameEmpire";
			this.gameEmpire.Size = new System.Drawing.Size(180, 22);
			this.gameEmpire.Text = "Empire";
			this.gameEmpire.Click += new System.EventHandler(this.gameEmpire_Click);
			this.gameNapoleon.CheckOnClick = true;
			this.gameNapoleon.Name = "gameNapoleon";
			this.gameNapoleon.Size = new System.Drawing.Size(180, 22);
			this.gameNapoleon.Text = "Napoleon";
			this.gameNapoleon.Click += new System.EventHandler(this.gameNapoleon_Click);
			this.gameShogun2.CheckOnClick = true;
			this.gameShogun2.Name = "gameShogun2";
			this.gameShogun2.Size = new System.Drawing.Size(180, 22);
			this.gameShogun2.Text = "Shogun 2";
			this.gameShogun2.Click += new System.EventHandler(this.gameShogun2_Click);
			this.throbGame.CheckOnClick = true;
			this.throbGame.Name = "throbGame";
			this.throbGame.Size = new System.Drawing.Size(180, 22);
			this.throbGame.Text = "Thrones of Britannia";
			this.throbGame.Click += new System.EventHandler(this.throbGame_Click);
			this.threeKingdomsGame.CheckOnClick = true;
			this.threeKingdomsGame.Name = "threeKingdomsGame";
			this.threeKingdomsGame.Size = new System.Drawing.Size(180, 22);
			this.threeKingdomsGame.Text = "Three Kingdoms";
			this.threeKingdomsGame.Click += new System.EventHandler(this.threeKingdomsGame_Click);
			this.troyGame.CheckOnClick = true;
			this.troyGame.Name = "troyGame";
			this.troyGame.Size = new System.Drawing.Size(180, 22);
			this.troyGame.Text = "Troy";
			this.troyGame.Click += new System.EventHandler(this.troyGame_Click);
			this.userScriptExtensionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.expandedDebugSettingsToolStripMenuItem });
			this.userScriptExtensionsToolStripMenuItem.Name = "userScriptExtensionsToolStripMenuItem";
			this.userScriptExtensionsToolStripMenuItem.Size = new System.Drawing.Size(134, 20);
			this.userScriptExtensionsToolStripMenuItem.Text = "User Script Extensions";
			this.userScriptExtensionsToolStripMenuItem.Visible = false;
			this.expandedDebugSettingsToolStripMenuItem.CheckOnClick = true;
			this.expandedDebugSettingsToolStripMenuItem.Name = "expandedDebugSettingsToolStripMenuItem";
			this.expandedDebugSettingsToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
			this.expandedDebugSettingsToolStripMenuItem.Text = "Expanded Debug Settings";
			this.labelDataPath.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			this.labelDataPath.Location = new System.Drawing.Point(898, 537);
			this.labelDataPath.Name = "labelDataPath";
			this.labelDataPath.Size = new System.Drawing.Size(12, 21);
			this.labelDataPath.TabIndex = 27;
			this.labelDataPath.Text = "C:\\";
			this.labelDataPath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.labelDataPath.Visible = false;
			this.BumpPriority.AutoSize = true;
			this.BumpPriority.Checked = true;
			this.BumpPriority.CheckState = System.Windows.Forms.CheckState.Checked;
			this.BumpPriority.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			this.BumpPriority.Location = new System.Drawing.Point(901, 496);
			this.BumpPriority.Name = "BumpPriority";
			this.BumpPriority.Size = new System.Drawing.Size(121, 24);
			this.BumpPriority.TabIndex = 31;
			this.BumpPriority.Text = "Bump Priority";
			this.toolTip1.SetToolTip(this.BumpPriority, "When an item is checked and you move it up for the first time, it will move the item either to the top of the list or at the end of the checked items.");
			this.BumpPriority.UseVisualStyleBackColor = true;
			this.BumpPriority.Visible = false;
			this.cleanDataButton.Font = new System.Drawing.Font("Verdana", 14.25f);
			this.cleanDataButton.Location = new System.Drawing.Point(901, 660);
			this.cleanDataButton.Name = "cleanDataButton";
			this.cleanDataButton.Size = new System.Drawing.Size(136, 38);
			this.cleanDataButton.TabIndex = 40;
			this.cleanDataButton.Text = "Clean Data";
			this.toolTip1.SetToolTip(this.cleanDataButton, "Clean out all mods from the Data folder");
			this.cleanDataButton.UseVisualStyleBackColor = true;
			this.cleanDataButton.Visible = false;
			this.cleanDataButton.Click += new System.EventHandler(this.cleanDataButton_Click);
			this.pictureBox1.Location = new System.Drawing.Point(901, 106);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(371, 363);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox1.TabIndex = 32;
			this.pictureBox1.TabStop = false;
			this.refreshManagerButton.Font = new System.Drawing.Font("Verdana", 14.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			this.refreshManagerButton.Location = new System.Drawing.Point(1057, 660);
			this.refreshManagerButton.Name = "refreshManagerButton";
			this.refreshManagerButton.Padding = new System.Windows.Forms.Padding(6, 0, 0, 0);
			this.refreshManagerButton.Size = new System.Drawing.Size(136, 38);
			this.refreshManagerButton.TabIndex = 33;
			this.refreshManagerButton.Text = "Refresh";
			this.refreshManagerButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.refreshManagerButton.UseVisualStyleBackColor = true;
			this.refreshManagerButton.Click += new System.EventHandler(this.refreshManagerButton_Click);
			this.nameColumn.Text = "Name";
			this.nameColumn.Width = 275;
			this.conflictColumn.Text = "Conflicts";
			this.conflictColumn.Width = 131;
			this.DateColumn.Text = "Date";
			this.DateColumn.Width = 371;
			this.listViewMod.Activation = System.Windows.Forms.ItemActivation.OneClick;
			this.listViewMod.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
			this.listViewMod.CheckBoxes = true;
			this.listViewMod.Columns.AddRange(new System.Windows.Forms.ColumnHeader[3] { this.nameColumn, this.conflictColumn, this.DateColumn });
			this.listViewMod.FullRowSelect = true;
			this.listViewMod.HideSelection = false;
			this.listViewMod.Location = new System.Drawing.Point(15, 106);
			this.listViewMod.MultiSelect = false;
			this.listViewMod.Name = "listViewMod";
			this.listViewMod.Size = new System.Drawing.Size(869, 684);
			this.listViewMod.TabIndex = 3;
			this.listViewMod.UseCompatibleStateImageBehavior = false;
			this.listViewMod.View = System.Windows.Forms.View.Details;
			this.listViewMod.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewMod_ColumnClick);
			this.listViewMod.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.listViewMod_ItemCheck);
			this.listViewMod.SelectedIndexChanged += new System.EventHandler(this.listViewMod_SelectedIndexChanged);
			this.listViewMod.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listViewMod_MouseClick);
			this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label2.Font = new System.Drawing.Font("Verdana", 12f, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, 0);
			this.label2.ForeColor = System.Drawing.Color.DarkRed;
			this.label2.Location = new System.Drawing.Point(462, 3);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(822, 30);
			this.label2.TabIndex = 35;
			this.label2.Text = "Make sure you unselect ALL mods in the CA mod manager before hitting the Launch button!";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.enableAllCheckBox.AutoSize = true;
			this.enableAllCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			this.enableAllCheckBox.Location = new System.Drawing.Point(202, 77);
			this.enableAllCheckBox.Name = "enableAllCheckBox";
			this.enableAllCheckBox.Size = new System.Drawing.Size(97, 24);
			this.enableAllCheckBox.TabIndex = 37;
			this.enableAllCheckBox.Text = "Toggle All";
			this.enableAllCheckBox.UseVisualStyleBackColor = true;
			this.enableAllCheckBox.CheckStateChanged += new System.EventHandler(this.enableAllCheckBox_CheckStateChanged);
			this.importProfile.Filter = "Text files (*.txt)|*.txt";
			this.importProfile.Title = "Import a Profile file to for personal use.";
			this.exportProfile.Filter = "Text files (*.txt)|*.txt";
			this.exportProfile.Title = "Export a Profile file for sharing.";
			this.labelModCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			this.labelModCount.Location = new System.Drawing.Point(326, 77);
			this.labelModCount.Name = "labelModCount";
			this.labelModCount.Size = new System.Drawing.Size(91, 22);
			this.labelModCount.TabIndex = 38;
			this.labelModCount.Text = "Mod Count:";
			this.labelModCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.labelModCountNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			this.labelModCountNum.Location = new System.Drawing.Point(423, 77);
			this.labelModCountNum.Name = "labelModCountNum";
			this.labelModCountNum.Size = new System.Drawing.Size(36, 22);
			this.labelModCountNum.TabIndex = 39;
			this.labelModCountNum.Text = "0";
			this.labelModCountNum.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[24]
			{
				this.showContentsToolStripMenuItem, this.openSteamPage2ToolStripMenuItem, this.openSteamPage3ToolStripMenuItem, this.openWithPFMToolStripMenuItem, this.openWithRPFMToolStripMenuItem, this.setUserNote2StripMenuItem3, this.toolStripSeparator1, this.SteamStripMenuItem, this.toolStripSeparator2, this.saveToArchiveToolStripMenuItem,
				this.restoreFromArchiveToolStripMenuItem, this.toolStripSeparator4, this.unsubscribeToolStripMenuItem, this.unsubscribeMultipleToolStripMenuItem, this.toolStripSeparator5, this.deleteDataToolStripMenuItem, this.deleteDataMultipleToolStripMenuItem, this.deleteContentStripMenuItem3, this.copyToolStripMenuItem, this.renameToolStripMenuItem,
				this.addModToExistingProfileToolStripMenuItem, this.setReadOnlyToolStripMenuItem, this.hideModFromProfileToolStripMenuItem, this.mergeSelectedModsToolStripMenuItem
			});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(395, 468);
			this.showContentsToolStripMenuItem.Name = "showContentsToolStripMenuItem";
			this.showContentsToolStripMenuItem.Size = new System.Drawing.Size(394, 22);
			this.showContentsToolStripMenuItem.Text = "Show Contents";
			this.showContentsToolStripMenuItem.Click += new System.EventHandler(this.showContentsToolStripMenuItem_Click);
			this.openSteamPage2ToolStripMenuItem.Name = "openSteamPage2ToolStripMenuItem";
			this.openSteamPage2ToolStripMenuItem.Size = new System.Drawing.Size(394, 22);
			this.openSteamPage2ToolStripMenuItem.Text = "Open Steam Page (via Steam)";
			this.openSteamPage2ToolStripMenuItem.Click += new System.EventHandler(this.openSteamPage2ToolStripMenuItem_Click);
			this.openSteamPage3ToolStripMenuItem.Name = "openSteamPage3ToolStripMenuItem";
			this.openSteamPage3ToolStripMenuItem.Size = new System.Drawing.Size(394, 22);
			this.openSteamPage3ToolStripMenuItem.Text = "Open Steam Page (via Web Browser)";
			this.openSteamPage3ToolStripMenuItem.Click += new System.EventHandler(this.openSteamPage3ToolStripMenuItem_Click);
			this.openWithPFMToolStripMenuItem.Name = "openWithPFMToolStripMenuItem";
			this.openWithPFMToolStripMenuItem.Size = new System.Drawing.Size(394, 22);
			this.openWithPFMToolStripMenuItem.Text = "Open with PFM";
			this.openWithPFMToolStripMenuItem.Click += new System.EventHandler(this.openWithPFMToolStripMenuItem_Click);
			this.openWithRPFMToolStripMenuItem.Name = "openWithRPFMToolStripMenuItem";
			this.openWithRPFMToolStripMenuItem.Size = new System.Drawing.Size(394, 22);
			this.openWithRPFMToolStripMenuItem.Text = "Open with RPFM";
			this.openWithRPFMToolStripMenuItem.Click += new System.EventHandler(this.openWithRPFMToolStripMenuItem_Click);
			this.setUserNote2StripMenuItem3.Name = "setUserNote2StripMenuItem3";
			this.setUserNote2StripMenuItem3.Size = new System.Drawing.Size(394, 22);
			this.setUserNote2StripMenuItem3.Text = "Set User Notes";
			this.setUserNote2StripMenuItem3.Visible = false;
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(391, 6);
			this.toolStripSeparator1.Visible = false;
			this.SteamStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[7] { this.openSteamPageToolStripMenuItem, this.voteUpToolStripMenuItem, this.downloadModAgainToolStripMenuItem, this.resubscribeToolStripMenuItem, this.toolStripSeparator3, this.updateModforModAuthorsToolStripMenuItem, this.uploadModToolStripMenuItem });
			this.SteamStripMenuItem.Name = "SteamStripMenuItem";
			this.SteamStripMenuItem.Size = new System.Drawing.Size(394, 22);
			this.SteamStripMenuItem.Text = "Steam";
			this.SteamStripMenuItem.Visible = false;
			this.openSteamPageToolStripMenuItem.Name = "openSteamPageToolStripMenuItem";
			this.openSteamPageToolStripMenuItem.Size = new System.Drawing.Size(237, 22);
			this.openSteamPageToolStripMenuItem.Text = "Open Steam Page";
			this.voteUpToolStripMenuItem.Name = "voteUpToolStripMenuItem";
			this.voteUpToolStripMenuItem.Size = new System.Drawing.Size(237, 22);
			this.voteUpToolStripMenuItem.Text = "Vote Up";
			this.downloadModAgainToolStripMenuItem.Name = "downloadModAgainToolStripMenuItem";
			this.downloadModAgainToolStripMenuItem.Size = new System.Drawing.Size(237, 22);
			this.downloadModAgainToolStripMenuItem.Text = "Download Mod Again";
			this.resubscribeToolStripMenuItem.Name = "resubscribeToolStripMenuItem";
			this.resubscribeToolStripMenuItem.Size = new System.Drawing.Size(237, 22);
			this.resubscribeToolStripMenuItem.Text = "Subscribe Again to Mod";
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(234, 6);
			this.updateModforModAuthorsToolStripMenuItem.Name = "updateModforModAuthorsToolStripMenuItem";
			this.updateModforModAuthorsToolStripMenuItem.Size = new System.Drawing.Size(237, 22);
			this.updateModforModAuthorsToolStripMenuItem.Text = "Update Mod (for Mod authors)";
			this.uploadModToolStripMenuItem.Name = "uploadModToolStripMenuItem";
			this.uploadModToolStripMenuItem.Size = new System.Drawing.Size(237, 22);
			this.uploadModToolStripMenuItem.Text = "Upload Mod (for Mod authors)";
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(391, 6);
			this.saveToArchiveToolStripMenuItem.Name = "saveToArchiveToolStripMenuItem";
			this.saveToArchiveToolStripMenuItem.Size = new System.Drawing.Size(394, 22);
			this.saveToArchiveToolStripMenuItem.Text = "Save to Archive";
			this.saveToArchiveToolStripMenuItem.Click += new System.EventHandler(this.saveToArchiveToolStripMenuItem_Click);
			this.restoreFromArchiveToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.versionDatedXToolStripMenuItem, this.versionDatedYToolStripMenuItem, this.versionDatedZToolStripMenuItem });
			this.restoreFromArchiveToolStripMenuItem.Name = "restoreFromArchiveToolStripMenuItem";
			this.restoreFromArchiveToolStripMenuItem.Size = new System.Drawing.Size(394, 22);
			this.restoreFromArchiveToolStripMenuItem.Text = "Restore from Archive";
			this.restoreFromArchiveToolStripMenuItem.MouseEnter += new System.EventHandler(this.restoreFromArchiveToolStripMenuItem_MouseEnter);
			this.versionDatedXToolStripMenuItem.Name = "versionDatedXToolStripMenuItem";
			this.versionDatedXToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
			this.versionDatedXToolStripMenuItem.Text = "Version dated : x";
			this.versionDatedXToolStripMenuItem.Click += new System.EventHandler(this.versionDatedXToolStripMenuItem_Click);
			this.versionDatedYToolStripMenuItem.Name = "versionDatedYToolStripMenuItem";
			this.versionDatedYToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
			this.versionDatedYToolStripMenuItem.Text = "Version dated : y";
			this.versionDatedYToolStripMenuItem.Visible = false;
			this.versionDatedZToolStripMenuItem.Name = "versionDatedZToolStripMenuItem";
			this.versionDatedZToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
			this.versionDatedZToolStripMenuItem.Text = "Version dated : z";
			this.versionDatedZToolStripMenuItem.Visible = false;
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(391, 6);
			this.unsubscribeToolStripMenuItem.Name = "unsubscribeToolStripMenuItem";
			this.unsubscribeToolStripMenuItem.Size = new System.Drawing.Size(394, 22);
			this.unsubscribeToolStripMenuItem.Text = "Unsubscribe from Steam and Delete";
			this.unsubscribeToolStripMenuItem.Click += new System.EventHandler(this.unsubscribeToolStripMenuItem_Click);
			this.unsubscribeMultipleToolStripMenuItem.Name = "unsubscribeMultipleToolStripMenuItem";
			this.unsubscribeMultipleToolStripMenuItem.Size = new System.Drawing.Size(394, 22);
			this.unsubscribeMultipleToolStripMenuItem.Text = "Unsubscribe from Steam and Delete (Multiple, Any Checked)";
			this.unsubscribeMultipleToolStripMenuItem.Click += new System.EventHandler(this.unsubscribeMultipleToolStripMenuItem_Click);
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(391, 6);
			this.deleteDataToolStripMenuItem.Name = "deleteDataToolStripMenuItem";
			this.deleteDataToolStripMenuItem.Size = new System.Drawing.Size(394, 22);
			this.deleteDataToolStripMenuItem.Text = "Delete from Data";
			this.deleteDataToolStripMenuItem.Click += new System.EventHandler(this.deleteDataToolStripMenuItem_Click);
			this.deleteDataMultipleToolStripMenuItem.Name = "deleteDataMultipleToolStripMenuItem";
			this.deleteDataMultipleToolStripMenuItem.Size = new System.Drawing.Size(394, 22);
			this.deleteDataMultipleToolStripMenuItem.Text = "Delete from Data (Multiple, Any Checked)";
			this.deleteDataMultipleToolStripMenuItem.Click += new System.EventHandler(this.deleteDataMultipleToolStripMenuItem_Click);
			this.deleteContentStripMenuItem3.Name = "deleteContentStripMenuItem3";
			this.deleteContentStripMenuItem3.Size = new System.Drawing.Size(394, 22);
			this.deleteContentStripMenuItem3.Text = "Delete from Content";
			this.deleteContentStripMenuItem3.ToolTipText = "This will force a download the next time you start the CA launcher.";
			this.deleteContentStripMenuItem3.Visible = false;
			this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			this.copyToolStripMenuItem.Size = new System.Drawing.Size(394, 22);
			this.copyToolStripMenuItem.Text = "Copy";
			this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
			this.renameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.toolStripTextBox1 });
			this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
			this.renameToolStripMenuItem.Size = new System.Drawing.Size(394, 22);
			this.renameToolStripMenuItem.Text = "Rename";
			this.renameToolStripMenuItem.MouseEnter += new System.EventHandler(this.renameToolStripMenuItem_MouseEnter);
			this.toolStripTextBox1.AutoSize = false;
			this.toolStripTextBox1.Name = "toolStripTextBox1";
			this.toolStripTextBox1.Size = new System.Drawing.Size(400, 23);
			this.toolStripTextBox1.MouseLeave += new System.EventHandler(this.toolStripTextBox1_MouseLeave);
			this.addModToExistingProfileToolStripMenuItem.Name = "addModToExistingProfileToolStripMenuItem";
			this.addModToExistingProfileToolStripMenuItem.Size = new System.Drawing.Size(394, 22);
			this.addModToExistingProfileToolStripMenuItem.Text = "Add Mod to Existing Profile";
			this.addModToExistingProfileToolStripMenuItem.Visible = false;
			this.setReadOnlyToolStripMenuItem.Name = "setReadOnlyToolStripMenuItem";
			this.setReadOnlyToolStripMenuItem.Size = new System.Drawing.Size(394, 22);
			this.setReadOnlyToolStripMenuItem.Text = "Set Read Only";
			this.setReadOnlyToolStripMenuItem.Visible = false;
			this.hideModFromProfileToolStripMenuItem.Name = "hideModFromProfileToolStripMenuItem";
			this.hideModFromProfileToolStripMenuItem.Size = new System.Drawing.Size(394, 22);
			this.hideModFromProfileToolStripMenuItem.Text = "Hide Mod from Profile";
			this.hideModFromProfileToolStripMenuItem.Visible = false;
			this.mergeSelectedModsToolStripMenuItem.Name = "mergeSelectedModsToolStripMenuItem";
			this.mergeSelectedModsToolStripMenuItem.Size = new System.Drawing.Size(394, 22);
			this.mergeSelectedModsToolStripMenuItem.Text = "Merge Selected Mods (Beta)";
			this.mergeSelectedModsToolStripMenuItem.Click += new System.EventHandler(this.mergeSelectedModsToolStripMenuItem_Click);
			this.openPackFileDialog.DefaultExt = "exe";
			this.openPackFileDialog.FileName = "PackFileManager.exe";
			this.openPackFileDialog.Filter = "Exe Files|*.exe";
			this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.label1.Location = new System.Drawing.Point(11, 36);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(353, 24);
			this.label1.TabIndex = 41;
			this.label1.Text = "Latest Version: ";
			this.linkLabel1.AutoSize = true;
			this.linkLabel1.Font = new System.Drawing.Font("Verdana", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			this.linkLabel1.Location = new System.Drawing.Point(921, 81);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(346, 16);
			this.linkLabel1.TabIndex = 42;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "Total War Mod Help Site and Troubleshooting Guide";
			this.linkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.label3.Location = new System.Drawing.Point(459, 83);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(456, 13);
			this.label3.TabIndex = 43;
			this.label3.Text = "Date Column: Blue/Green for updated in the last 7 days, Orange for out of date";
			this.labelEnabledMovies.Font = new System.Drawing.Font("Arial", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			this.labelEnabledMovies.Location = new System.Drawing.Point(1054, 636);
			this.labelEnabledMovies.Name = "labelEnabledMovies";
			this.labelEnabledMovies.Size = new System.Drawing.Size(139, 21);
			this.labelEnabledMovies.TabIndex = 44;
			this.labelEnabledMovies.Text = "Enabled Movies: 0";
			this.labelEnabledMovies.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.labelEnabledMovies.Visible = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			base.ClientSize = new System.Drawing.Size(1284, 802);
			base.Controls.Add(this.labelEnabledMovies);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.linkLabel1);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.cleanDataButton);
			base.Controls.Add(this.labelModCountNum);
			base.Controls.Add(this.labelModCount);
			base.Controls.Add(this.enableAllCheckBox);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.refreshManagerButton);
			base.Controls.Add(this.pictureBox1);
			base.Controls.Add(this.BumpPriority);
			base.Controls.Add(this.labelDataPath);
			base.Controls.Add(this.labelOrderInfo);
			base.Controls.Add(this.moveUpButton);
			base.Controls.Add(this.moveDownButton);
			base.Controls.Add(this.groupBoxProfiles);
			base.Controls.Add(this.labelMod);
			base.Controls.Add(this.launchGameButton);
			base.Controls.Add(this.listViewMod);
			base.Controls.Add(this.menuStrip);
			this.DoubleBuffered = true;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MainMenuStrip = this.menuStrip;
			base.Name = "FormMain";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Total War Mod Manager v1.9.0";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
			base.Load += new System.EventHandler(this.FormMain_Load);
			base.Shown += new System.EventHandler(this.FormMain_Shown);
			this.groupBoxProfiles.ResumeLayout(false);
			this.groupBoxProfiles.PerformLayout();
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
			this.contextMenuStrip1.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void enableAllCheckBox_CheckStateChanged(object sender, EventArgs e)
		{
			if (!beingReset)
			{
				if (enableAllCheckBox.Checked)
				{
					foreach (ListViewItem item in listViewMod.Items)
					{
						item.Checked = true;
					}
				}
				else
				{
					foreach (ListViewItem item2 in listViewMod.Items)
					{
						item2.Checked = false;
					}
				}
			}
			profilesComboBox.Text = "";
		}

		protected async Task<string> AsyncAwait_GetSomeDataAsync()
		{
			ServicePointManager.Expect100Continue = true;
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
			HttpResponseMessage httpResponseMessage = await new HttpClient
			{
				Timeout = new TimeSpan(0, 0, 5)
			}.GetAsync("https://raw.githubusercontent.com/Kaedrin/warhammer-mod-manager/master/KMMVersion.txt");
			httpResponseMessage.EnsureSuccessStatusCode();
			return await httpResponseMessage.Content.ReadAsStringAsync();
		}

		private async void UpdateLabel()
		{
			try
			{
				string text = "Latest Version: 1.9.0";
				label1.Text = text;
			}
			catch
			{
			}
		}

		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start("https://tw-modding.com/index.php/Main_Page");
		}

		private void moveUpButton_Click(object sender, EventArgs e)
		{
			int num = 0;
			if (listViewMod.SelectedIndices.Count <= 0)
			{
				return;
			}
			int num2 = -1;
			for (int i = 0; i < listViewMod.Items.Count && listViewMod.Items[i].Checked; i++)
			{
				num2 = i;
			}
			int num3 = listViewMod.SelectedIndices[0];
			ListViewItem item = listViewMod.SelectedItems[0];
			bool flag = listViewMod.Items[num3].Checked;
			listViewMod.Items.RemoveAt(num3);
			if (flag && BumpPriority.Checked)
			{
				if (num3 > num2)
				{
					num = num2 + 1;
					if (num < 0)
					{
						num = 0;
					}
					listViewMod.Items.Insert(num, item);
					listViewMod.Items[num].Checked = flag;
					listViewMod.Items[num].Selected = true;
				}
				else
				{
					num = num3 - 1;
					if (num < 0)
					{
						num = 0;
					}
					listViewMod.Items.Insert(num, item);
					listViewMod.Items[num].Checked = flag;
					listViewMod.Items[num].Selected = true;
				}
			}
			else
			{
				num = num3 - 1;
				if (num < 0)
				{
					num = 0;
				}
				listViewMod.Items.Insert(num, item);
				listViewMod.Items[num].Checked = flag;
				listViewMod.Items[num].Selected = true;
			}
			listViewMod.Focus();
		}

		private void moveDownButton_Click(object sender, EventArgs e)
		{
			if (listViewMod.SelectedIndices.Count > 0)
			{
				int num = listViewMod.SelectedIndices[0];
				if (num != listViewMod.Items.Count - 1)
				{
					ListViewItem item = listViewMod.SelectedItems[0];
					bool flag = listViewMod.Items[num].Checked;
					listViewMod.Items.RemoveAt(num);
					listViewMod.Items.Insert(num + 1, item);
					listViewMod.Items[num + 1].Selected = true;
					listViewMod.Items[num + 1].Checked = flag;
					listViewMod.Focus();
				}
			}
		}

		private void useLastProfileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			useLastProfile = useLastProfileMenuItem.Checked;
		}

		private void cleanDataToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CleanData();
		}

		private void highlightRecentModsMenuItem_Click(object sender, EventArgs e)
		{
			if (highlightRecentModsMenuItem.Checked)
			{
				highlightRecentMods = true;
			}
			else
			{
				highlightRecentMods = false;
			}
		}

		private void gameAttila_Click(object sender, EventArgs e)
		{
			bool flag = ((ToolStripMenuItem)sender).Checked;
			ToolStripMenuItem toolStripMenuItem = ((ToolStripMenuItem)sender).OwnerItem as ToolStripMenuItem;
			if (toolStripMenuItem != null)
			{
				foreach (ToolStripMenuItem dropDownItem in toolStripMenuItem.DropDownItems)
				{
					if (dropDownItem.Enabled)
					{
						dropDownItem.Checked = false;
					}
				}
			}
			if (flag)
			{
				((ToolStripMenuItem)sender).Checked = true;
			}
			else if (lastChecked == gameAttila.Name)
			{
				((ToolStripMenuItem)sender).Checked = true;
			}
			setGetFileData(gameAttila.Name);
		}

		private void gameRome2_Click(object sender, EventArgs e)
		{
			bool flag = ((ToolStripMenuItem)sender).Checked;
			ToolStripMenuItem toolStripMenuItem = ((ToolStripMenuItem)sender).OwnerItem as ToolStripMenuItem;
			if (toolStripMenuItem != null)
			{
				foreach (ToolStripMenuItem dropDownItem in toolStripMenuItem.DropDownItems)
				{
					if (dropDownItem.Enabled)
					{
						dropDownItem.Checked = false;
					}
				}
			}
			if (flag)
			{
				((ToolStripMenuItem)sender).Checked = true;
			}
			else if (lastChecked == gameRome2.Name)
			{
				((ToolStripMenuItem)sender).Checked = true;
			}
			setGetFileData(gameRome2.Name);
		}

		private void gameEmpire_Click(object sender, EventArgs e)
		{
			bool flag = ((ToolStripMenuItem)sender).Checked;
			ToolStripMenuItem toolStripMenuItem = ((ToolStripMenuItem)sender).OwnerItem as ToolStripMenuItem;
			if (toolStripMenuItem != null)
			{
				foreach (ToolStripMenuItem dropDownItem in toolStripMenuItem.DropDownItems)
				{
					if (dropDownItem.Enabled)
					{
						dropDownItem.Checked = false;
					}
				}
			}
			if (flag)
			{
				((ToolStripMenuItem)sender).Checked = true;
			}
			else if (lastChecked == gameEmpire.Name)
			{
				((ToolStripMenuItem)sender).Checked = true;
			}
			setGetFileData(gameEmpire.Name);
		}

		private void gameNapoleon_Click(object sender, EventArgs e)
		{
			bool flag = ((ToolStripMenuItem)sender).Checked;
			ToolStripMenuItem toolStripMenuItem = ((ToolStripMenuItem)sender).OwnerItem as ToolStripMenuItem;
			if (toolStripMenuItem != null)
			{
				foreach (ToolStripMenuItem dropDownItem in toolStripMenuItem.DropDownItems)
				{
					if (dropDownItem.Enabled)
					{
						dropDownItem.Checked = false;
					}
				}
			}
			if (flag)
			{
				((ToolStripMenuItem)sender).Checked = true;
			}
			else if (lastChecked == gameNapoleon.Name)
			{
				((ToolStripMenuItem)sender).Checked = true;
			}
			setGetFileData(gameNapoleon.Name);
		}

		private void gameShogun2_Click(object sender, EventArgs e)
		{
			bool flag = ((ToolStripMenuItem)sender).Checked;
			ToolStripMenuItem toolStripMenuItem = ((ToolStripMenuItem)sender).OwnerItem as ToolStripMenuItem;
			if (toolStripMenuItem != null)
			{
				foreach (ToolStripMenuItem dropDownItem in toolStripMenuItem.DropDownItems)
				{
					if (dropDownItem.Enabled)
					{
						dropDownItem.Checked = false;
					}
				}
			}
			if (flag)
			{
				((ToolStripMenuItem)sender).Checked = true;
			}
			else if (lastChecked == gameShogun2.Name)
			{
				((ToolStripMenuItem)sender).Checked = true;
			}
			setGetFileData(gameShogun2.Name);
		}

		private void warhammer3Game_Click(object sender, EventArgs e)
		{
			bool flag = ((ToolStripMenuItem)sender).Checked;
			ToolStripMenuItem toolStripMenuItem = ((ToolStripMenuItem)sender).OwnerItem as ToolStripMenuItem;
			if (toolStripMenuItem != null)
			{
				foreach (ToolStripMenuItem dropDownItem in toolStripMenuItem.DropDownItems)
				{
					if (dropDownItem.Enabled)
					{
						dropDownItem.Checked = false;
					}
				}
			}
			if (flag)
			{
				((ToolStripMenuItem)sender).Checked = true;
			}
			else if (lastChecked == warhammer3Game.Name)
			{
				((ToolStripMenuItem)sender).Checked = true;
			}
			setGetFileData(warhammer3Game.Name);
		}

		private void warhammer2Game_Click(object sender, EventArgs e)
		{
			bool flag = ((ToolStripMenuItem)sender).Checked;
			ToolStripMenuItem toolStripMenuItem = ((ToolStripMenuItem)sender).OwnerItem as ToolStripMenuItem;
			if (toolStripMenuItem != null)
			{
				foreach (ToolStripMenuItem dropDownItem in toolStripMenuItem.DropDownItems)
				{
					if (dropDownItem.Enabled)
					{
						dropDownItem.Checked = false;
					}
				}
			}
			if (flag)
			{
				((ToolStripMenuItem)sender).Checked = true;
			}
			else if (lastChecked == warhammer2Game.Name)
			{
				((ToolStripMenuItem)sender).Checked = true;
			}
			setGetFileData(warhammer2Game.Name);
		}

		private void warhammerGame_Click(object sender, EventArgs e)
		{
			bool flag = ((ToolStripMenuItem)sender).Checked;
			ToolStripMenuItem toolStripMenuItem = ((ToolStripMenuItem)sender).OwnerItem as ToolStripMenuItem;
			if (toolStripMenuItem != null)
			{
				foreach (ToolStripMenuItem dropDownItem in toolStripMenuItem.DropDownItems)
				{
					if (dropDownItem.Enabled)
					{
						dropDownItem.Checked = false;
					}
				}
			}
			if (flag)
			{
				((ToolStripMenuItem)sender).Checked = true;
			}
			else if (lastChecked == warhammerGame.Name)
			{
				((ToolStripMenuItem)sender).Checked = true;
			}
			setGetFileData(warhammerGame.Name);
		}

		private void throbGame_Click(object sender, EventArgs e)
		{
			bool flag = ((ToolStripMenuItem)sender).Checked;
			ToolStripMenuItem toolStripMenuItem = ((ToolStripMenuItem)sender).OwnerItem as ToolStripMenuItem;
			if (toolStripMenuItem != null)
			{
				foreach (ToolStripMenuItem dropDownItem in toolStripMenuItem.DropDownItems)
				{
					if (dropDownItem.Enabled)
					{
						dropDownItem.Checked = false;
					}
				}
			}
			if (flag)
			{
				((ToolStripMenuItem)sender).Checked = true;
			}
			else if (lastChecked == throbGame.Name)
			{
				((ToolStripMenuItem)sender).Checked = true;
			}
			setGetFileData(throbGame.Name);
		}

		private void threeKingdomsGame_Click(object sender, EventArgs e)
		{
			bool flag = ((ToolStripMenuItem)sender).Checked;
			ToolStripMenuItem toolStripMenuItem = ((ToolStripMenuItem)sender).OwnerItem as ToolStripMenuItem;
			if (toolStripMenuItem != null)
			{
				foreach (ToolStripMenuItem dropDownItem in toolStripMenuItem.DropDownItems)
				{
					if (dropDownItem.Enabled)
					{
						dropDownItem.Checked = false;
					}
				}
			}
			if (flag)
			{
				((ToolStripMenuItem)sender).Checked = true;
			}
			else if (lastChecked == threeKingdomsGame.Name)
			{
				((ToolStripMenuItem)sender).Checked = true;
			}
			setGetFileData(threeKingdomsGame.Name);
		}

		private void troyGame_Click(object sender, EventArgs e)
		{
			bool flag = ((ToolStripMenuItem)sender).Checked;
			ToolStripMenuItem toolStripMenuItem = ((ToolStripMenuItem)sender).OwnerItem as ToolStripMenuItem;
			if (toolStripMenuItem != null)
			{
				foreach (ToolStripMenuItem dropDownItem in toolStripMenuItem.DropDownItems)
				{
					if (dropDownItem.Enabled)
					{
						dropDownItem.Checked = false;
					}
				}
			}
			if (flag)
			{
				((ToolStripMenuItem)sender).Checked = true;
			}
			else if (lastChecked == troyGame.Name)
			{
				((ToolStripMenuItem)sender).Checked = true;
			}
			setGetFileData(troyGame.Name);
		}

		private void aboutWarhammerModManager_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Courtesy of Kaedrin. Email me at Kaedrin.nwn2@gmail.com if you encounter any issues. Thanks and Enjoy!\nSpecial thanks to ColinBencrofftheEstalianOverlord for help with testing!");
		}

		private void dataPath_Click(object sender, EventArgs e)
		{
			MessageBox.Show(labelDataPath.Text + "\nClick 'Revert to Vanilla' delete the current application preferences, then restart the mod manager to have the application rescan for a new install location.");
		}

		private void exportProfileToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (profilesComboBox.Text != "")
			{
				try
				{
					SteamTestProjects steamTestProjects = new SteamTestProjects();
					List<ulong> list = new List<ulong>();
					foreach (ListViewItem checkedItem in listViewMod.CheckedItems)
					{
						if (checkedItem != null)
						{
							SteamData value = null;
							steamDictionary.TryGetValue(checkedItem.Text, out value);
							if (value != null)
							{
								list.Add(value.SteamID);
							}
						}
					}
					ulong collectionID = steamTestProjects.CreateCollection();
					steamTestProjects.UpdateCollection(profilesComboBox.Text + " Profile", collectionID, list);
					return;
				}
				catch
				{
					return;
				}
			}
			MessageBox.Show("You must select one of your profiles to export first.");
		}

		private void exportProfileWithSteamXMLToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Regex regex = new Regex("(https?://[^\\s]+)");
			if (profilesComboBox.Text != "")
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog();
				saveFileDialog.FileName = "profile_" + profilesComboBox.Text + ".html";
				if (saveFileDialog.ShowDialog() != DialogResult.OK || !(saveFileDialog.FileName != ""))
				{
					return;
				}
				try
				{
					List<string> list = new List<string>();
					list.Add("<html>");
					list.Add("<body>");
					list.Add("<h1>" + profilesComboBox.Text + "</h1>");
					int num = 1;
					foreach (ListViewItem checkedItem in listViewMod.CheckedItems)
					{
						if (checkedItem != null)
						{
							SteamData value = null;
							steamDictionary.TryGetValue(checkedItem.Text, out value);
							if (value != null)
							{
								list.Add("<br><br>" + num + ". ");
								list.Add("File Name: " + value.HakFileName);
								list.Add("<br>Title: " + value.SteamTitle);
								list.Add("<br>Author: " + value.SteamAuthor);
								string text = regex.Replace(value.SteamURL, "<a href=\"$1\">$1</a>");
								list.Add("<br>URL: " + text);
								num++;
							}
							else
							{
								list.Add("<br><br>" + num + ". ");
								list.Add("File Name: " + checkedItem.Text);
								num++;
							}
						}
					}
					list.Add("</body>");
					list.Add("</html>");
					File.WriteAllLines(saveFileDialog.FileName, list);
					return;
				}
				catch
				{
					return;
				}
			}
			MessageBox.Show("You must select one of your profiles to export first.");
		}

		private void exportAllModsWithSteamURLsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Regex regex = new Regex("(https?://[^\\s]+)");
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.FileName = "FullModExport.html";
			if (saveFileDialog.ShowDialog() != DialogResult.OK || !(saveFileDialog.FileName != ""))
			{
				return;
			}
			try
			{
				List<string> list = new List<string>();
				list.Add("<html>");
				list.Add("<body>");
				list.Add("<h1>" + profilesComboBox.Text + "</h1>");
				int num = 1;
				SteamData steamData = null;
				foreach (KeyValuePair<string, SteamData> item in new SortedDictionary<string, SteamData>(steamDictionary))
				{
					steamData = item.Value;
					if (steamData != null)
					{
						list.Add("<br><br>" + num + ". ");
						list.Add("File Name: " + steamData.HakFileName);
						list.Add("<br>Title: " + steamData.SteamTitle);
						list.Add("<br>Author: " + steamData.SteamAuthor);
						string text = regex.Replace(steamData.SteamURL, "<a href=\"$1\">$1</a>");
						list.Add("<br>URL: " + text);
						num++;
					}
					else
					{
						list.Add("<br><br>" + num + ". ");
						list.Add("File Name: " + item.Key);
						num++;
					}
				}
				list.Add("</body>");
				list.Add("</html>");
				File.WriteAllLines(saveFileDialog.FileName, list);
			}
			catch
			{
			}
		}

		private void exportCheckedModsToHTMLToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Regex regex = new Regex("(https?://[^\\s]+)");
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.FileName = "FullModExport.html";
			if (saveFileDialog.ShowDialog() != DialogResult.OK || !(saveFileDialog.FileName != ""))
			{
				return;
			}
			try
			{
				List<string> list = new List<string>();
				list.Add("<html>");
				list.Add("<body>");
				list.Add("<h1>" + profilesComboBox.Text + "</h1>");
				int num = 1;
				SteamData steamData = null;
				foreach (KeyValuePair<string, SteamData> item in new SortedDictionary<string, SteamData>(steamDictionary))
				{
					steamData = item.Value;
					if (steamData != null)
					{
						list.Add("<br><br>" + num + ". ");
						list.Add("File Name: " + steamData.HakFileName);
						list.Add("<br>Title: " + steamData.SteamTitle);
						list.Add("<br>Author: " + steamData.SteamAuthor);
						string text = regex.Replace(steamData.SteamURL, "<a href=\"$1\">$1</a>");
						list.Add("<br>URL: " + text);
						num++;
					}
					else
					{
						list.Add("<br><br>" + num + ". ");
						list.Add("File Name: " + item.Key);
						num++;
					}
				}
				list.Add("</body>");
				list.Add("</html>");
				File.WriteAllLines(saveFileDialog.FileName, list);
			}
			catch
			{
			}
		}

		private void importProfileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (importProfile.ShowDialog() == DialogResult.OK)
			{
				string value = File.ReadAllText(importProfile.FileName);
				string path = CoreData.profilePath + "\\" + importProfile.SafeFileName;
				if (File.Exists(path))
				{
					File.Delete(path);
				}
				Encoding encoding = new UTF8Encoding(false);
				using (StreamWriter streamWriter = new StreamWriter(path, false, encoding))
				{
					streamWriter.WriteLine(value);
				}
				LoadProfiles();
			}
		}

		private void exportProfileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (profilesComboBox.Text != "")
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog();
				saveFileDialog.FileName = "profile_" + profilesComboBox.Text + ".txt";
				if (saveFileDialog.ShowDialog() == DialogResult.OK && saveFileDialog.FileName != "")
				{
					try
					{
						File.Copy(CoreData.profilePath + "\\profile_" + profilesComboBox.Text + ".txt", saveFileDialog.FileName, true);
					}
					catch
					{
					}
				}
			}
			else
			{
				MessageBox.Show("You must select one of your profiles to export first.");
			}
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Application.Exit();
			if (!bypassCALauncher)
			{
				return;
			}
			SteamAPI.Shutdown();
			try
			{
				if (File.Exists(Application.StartupPath + "\\steam_appid.txt"))
				{
					File.Delete(Application.StartupPath + "\\steam_appid.txt");
				}
			}
			catch
			{
			}
		}

		private void revertToVanillaToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (File.Exists(CoreData.userScriptFilepath))
			{
				File.Delete(CoreData.userScriptFilepath);
			}
			if (File.Exists(CoreData.myAppDataPath + "\\mod_manager_settings_v2.xml"))
			{
				File.Delete(CoreData.myAppDataPath + "\\mod_manager_settings_v2.xml");
			}
			if (File.Exists(CoreData.myAppDataPath + "\\mod_manager_settings_v3.xml"))
			{
				File.Delete(CoreData.myAppDataPath + "\\mod_manager_settings_v3.xml");
			}
		}

		private void chooseBackupLocationToolStripMenuItem_Click(object sender, EventArgs e)
		{
			findW2TWPathFolderBrowserDialog.Description = "Please select the folder where you wish to have a backup copy of your mods stored.";
			if (findW2TWPathFolderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				corePrefs.pathBackup = findW2TWPathFolderBrowserDialog.SelectedPath;
				if (!corePrefs.pathBackup.EndsWith("\\"))
				{
					corePrefs.pathBackup += "\\";
				}
				RefreshApp();
			}
		}

		private void choosePackFileManagerLocationToolStripMenuItem_Click(object sender, EventArgs e)
		{
			openPackFileDialog.Title = "Please select the folder where you have PackFileManager.exe located for the Pack File Manager.";
			openPackFileDialog.FileName = "PackFileManager.exe";
			if (openPackFileDialog.ShowDialog() == DialogResult.OK)
			{
				corePrefs.pathPFM = openPackFileDialog.FileName;
			}
		}

		private void chooseRustysPackFileManagerLocationToolStripMenuItem_Click(object sender, EventArgs e)
		{
			openPackFileDialog.Title = "Please select the folder where you have rpfm-code.exe located for the Rusted Pack File Manager.";
			openPackFileDialog.FileName = "rpfm-code.exe";
			if (openPackFileDialog.ShowDialog() == DialogResult.OK)
			{
				corePrefs.pathRPFM = openPackFileDialog.FileName;
			}
		}

		private void detectGameInstallationsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				File.Delete(CoreData.installPaths);
				WriteInstallPaths();
			}
			catch
			{
			}
		}

		private void bypassCALauncherBetaToolStripMenuItem_Click(object sender, EventArgs e)
		{
			bypassCALauncher = bypassCALauncherBetaToolStripMenuItem.Checked;
		}

		private void bypassConflictDetectionToolStripMenuItem_Click(object sender, EventArgs e)
		{
			bypassConflictDetect = bypassConflictDetectionToolStripMenuItem.Checked;
			if (bypassConflictDetect)
			{
				lvwColumnSorter.DateColumn = 1;
			}
			else
			{
				lvwColumnSorter.DateColumn = 2;
			}
		}

		private void backupModsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (backupModsToolStripMenuItem.Checked && corePrefs.pathBackup != null)
			{
				RefreshApp();
			}
		}

		private void openContentFolderInFileExplorerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			int length = tww2Path.IndexOf("common");
			string text = tww2Path.Substring(0, length) + "workshop\\content\\" + CoreData.appIDString;
			try
			{
				if (Directory.Exists(text))
				{
					Process.Start(new ProcessStartInfo
					{
						Arguments = text,
						FileName = "explorer.exe"
					});
				}
				else
				{
					MessageBox.Show(string.Format("{0} Directory does not exist!", text));
				}
			}
			catch
			{
			}
		}

		private void openDataFolderInFileExplorerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string text = tww2DataPath;
			try
			{
				if (Directory.Exists(text))
				{
					Process.Start(new ProcessStartInfo
					{
						Arguments = text,
						FileName = "explorer.exe"
					});
				}
				else
				{
					MessageBox.Show(string.Format("{0} Directory does not exist!", text));
				}
			}
			catch
			{
			}
		}

		private void cleanDataAndExitApplicationToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CleanData();
			Application.Exit();
			if (!bypassCALauncher)
			{
				return;
			}
			SteamAPI.Shutdown();
			try
			{
				if (File.Exists(Application.StartupPath + "\\steam_appid.txt"))
				{
					File.Delete(Application.StartupPath + "\\steam_appid.txt");
				}
			}
			catch
			{
			}
		}

		private void unsubscribeMultipleToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DialogResult dialogResult = MessageBox.Show("Are you sure you want to unsubscribe from all of the checked mods? The mods will be unsubscribed from on Steam and then removed from the Data folder.", "Unsubscribe En Masse!", MessageBoxButtons.YesNo);
			if (dialogResult == DialogResult.Yes)
			{
				if (!File.Exists(CoreData.appIdPath))
				{
					Encoding encoding = new UTF8Encoding(false);
					using (StreamWriter streamWriter = new StreamWriter(CoreData.appIdPath, false, encoding))
					{
						streamWriter.WriteLine(CoreData.appIDString);
						streamWriter.Close();
					}
				}
				if (!SteamAPIWrapper.Init())
				{
					MessageBox.Show("Steam must be running in order for this feature to work. The manager will now exit. Please start Steam and then start the manager.");
					Application.Exit();
					Environment.Exit(0);
				}
				string text = "";
				try
				{
					PublishedFileId_t nPublishedFileID = default(PublishedFileId_t);
					foreach (ListViewItem checkedItem in listViewMod.CheckedItems)
					{
						if (checkedItem == null)
						{
							continue;
						}
						int num = -1;
						for (int i = 0; i < modPacks.Count; i++)
						{
							if (!(modPacks[i].Name == checkedItem.Text))
							{
								continue;
							}
							num = i;
							try
							{
								if (num > -1)
								{
									SteamData value = null;
									steamDictionary.TryGetValue(modPacks[num].Name, out value);
									nPublishedFileID.m_PublishedFileId = value.SteamID;
									SteamUGC.UnsubscribeItem(nPublishedFileID);
									SteamAPIWrapper.RunCallbacks();
									text = modPacks[num].FilePath.Replace(".pack", ".png");
									File.Delete(modPacks[num].FilePath);
									if (File.Exists(text))
									{
										File.Delete(text);
									}
									int length = tww2Path.IndexOf("common");
									Directory.Delete(string.Concat(string.Concat(tww2Path.Substring(0, length) + "workshop\\content\\" + CoreData.appIDString, "\\"), value.SteamID.ToString()), true);
								}
							}
							catch
							{
							}
							break;
						}
					}
					RefreshApp();
					return;
				}
				catch
				{
					return;
				}
			}
			int num2 = 7;
		}

		private void deleteDataMultipleToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				string text = "";
				foreach (ListViewItem checkedItem in listViewMod.CheckedItems)
				{
					if (checkedItem == null)
					{
						continue;
					}
					int num = -1;
					for (int i = 0; i < modPacks.Count; i++)
					{
						if (!(modPacks[i].Name == checkedItem.Text))
						{
							continue;
						}
						num = i;
						try
						{
							text = modPacks[num].FilePath.Replace(".pack", ".png");
							File.Delete(modPacks[num].FilePath);
							if (File.Exists(text))
							{
								File.Delete(text);
							}
						}
						catch
						{
						}
						break;
					}
				}
				RefreshApp();
			}
			catch
			{
			}
		}

		private void enableDarkModeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			darkMode = enableDarkModeToolStripMenuItem.Checked;
		}

		private void enableForceSteamDownloadsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			forceSteamDownloads = enableForceSteamDownloadsToolStripMenuItem.Checked;
		}

		private void enableDeleteWhileForcingSteamDownloadsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			deleteWhileForcingSteamDownloads = enableDeleteWhileForcingSteamDownloadsToolStripMenuItem.Checked;
		}

		private bool PackContainsLocalizationFiles(Pack pack)
		{
			foreach (string file in pack.FileList)
			{
				if (Path.GetExtension(file) == ".loc")
				{
					return true;
				}
			}
			return false;
		}

		private List<string> GetPackContents(string file)
		{
			List<string> list = new List<string>();
			using (BinaryReader binaryReader = new BinaryReader(new FileStream(file, FileMode.Open), Encoding.ASCII))
			{
				PackHeader packHeader = ReadHeader(binaryReader);
				long dataStart = packHeader.DataStart;
				for (int i = 0; i < packHeader.FileCount; i++)
				{
					binaryReader.ReadUInt32();
					PackType packType = packHeader.PackType;
					if ((uint)(packType - 64) <= 2u)
					{
						binaryReader.ReadInt64();
					}
					string text = ReadZeroTerminatedAscii(binaryReader);
					text = text.Replace('\\', Path.DirectorySeparatorChar);
					if (text != "")
					{
						list.Add(text);
					}
					else
					{
						i--;
					}
				}
				return list;
			}
		}

		private List<string> ListActivatedMods()
		{
			List<string> list = new List<string>();
			for (int i = 0; i < listViewMod.Items.Count; i++)
			{
				if (listViewMod.Items[i].Checked)
				{
					list.Add(listViewMod.Items[i].Text);
				}
			}
			return list;
		}

		private void WriteProfile(string profileName)
		{
			if (ListActivatedMods().Count <= 0)
			{
				return;
			}
			profilesComboBox.SelectedIndexChanged -= profilesComboBox_SelectedIndexChanged;
			if (profilesComboBox.Text == "")
			{
				profilesComboBox.Text = profileName;
			}
			profilesComboBox.SelectedIndexChanged += profilesComboBox_SelectedIndexChanged;
			if (string.IsNullOrEmpty(profilesComboBox.Text))
			{
				return;
			}
			string path = CoreData.profilePath + "\\profile_" + profileName + ".txt";
			if (File.Exists(path))
			{
				File.Delete(path);
			}
			Encoding encoding = new UTF8Encoding(false);
			using (StreamWriter streamWriter = new StreamWriter(path, false, encoding))
			{
				foreach (ListViewItem item in listViewMod.Items)
				{
					if (item.Checked)
					{
						streamWriter.Write("{1}{0}\n", item.Text, item.Checked ? "" : "#");
					}
				}
			}
		}

		private void LoadProfiles()
		{
			int count = profilesComboBox.Items.Count;
			string text = profilesComboBox.Text;
			profilesComboBox.Items.Clear();
			foreach (string item in Directory.EnumerateFiles(CoreData.profilePath, "profile_*.txt"))
			{
				profilesComboBox.Items.Add(Regex.Match(item, "profile_(.+)\\.txt").Groups[1].ToString());
			}
			labelModCountNum.Text = "0";
			// FIX: on startup the combo text is empty, so the saved profile was never selected.
			// Prefer the last used profile, then the current selection, and fall back to "LastUsedMods".
			string wanted = (useLastProfile && !string.IsNullOrEmpty(profileNameUsed)) ? profileNameUsed : text;
			int num = string.IsNullOrEmpty(wanted) ? -1 : profilesComboBox.Items.IndexOf(wanted);
			if (num == -1)
			{
				num = profilesComboBox.Items.IndexOf("LastUsedMods");
			}
			if (num != -1)
			{
				profilesComboBox.SelectedIndex = num;
			}
		}

		private void ActivateProfile(string profileName)
		{
			lvwColumnSorter.Order = SortOrder.Ascending;
			lvwColumnSorter.SortColumn = 0;
			listViewMod.Sort();
			beingReset = true;
			enableAllCheckBox.Checked = false;
			listViewMod.BeginUpdate();
			listViewMod.Items.Clear();
			ListAllMods();
			ColourListViewConflicts();
			listViewMod.EndUpdate();
			beingReset = false;
			string path = CoreData.profilePath + "\\profile_" + profileName + ".txt";
			lvwColumnSorter.Order = SortOrder.None;
			if (!File.Exists(path))
			{
				return;
			}
			List<string> list = new List<string>(File.ReadAllLines(path));
			List<ProfileMod> list2 = new List<ProfileMod>();
			for (int i = 0; i < list.Count; i++)
			{
				bool flag = true;
				if (list[i].StartsWith("#"))
				{
					flag = false;
					list[i] = list[i].Remove(0, 1);
				}
				if (flag)
				{
					ProfileMod item = new ProfileMod(list[i], i, flag);
					list2.Add(item);
				}
			}
			new List<string>();
			if (pictureBox1.Image != null)
			{
				pictureBox1.Image.Dispose();
				pictureBox1.Image = null;
			}
			int num = 0;
			for (int j = 0; j < list2.Count; j++)
			{
				foreach (ListViewItem item2 in listViewMod.Items)
				{
					if (list2[j].Title == item2.Text)
					{
						listViewMod.Items.RemoveAt(item2.Index);
						listViewMod.Items.Insert(num, item2);
						listViewMod.Items[num].Selected = false;
						num++;
						break;
					}
				}
			}
			for (int k = 0; k < num; k++)
			{
				listViewMod.Items[k].Checked = true;
			}
			listViewMod.Select();
			listViewMod.Focus();
		}

		private void saveProfileButton_Click(object sender, EventArgs e)
		{
			WriteProfile(profilesComboBox.Text);
			if (!profilesComboBox.Items.Contains(profilesComboBox.Text))
			{
				profilesComboBox.Items.Add(profilesComboBox.Text);
			}
		}

		private void deleteProfileButton_Click(object sender, EventArgs e)
		{
			if (profilesComboBox.Text != "")
			{
				string path = CoreData.profilePath + "\\profile_" + profilesComboBox.Text + ".txt";
				if (File.Exists(path))
				{
					File.Delete(path);
				}
				profilesComboBox.Items.Remove(profilesComboBox.Text);
				profilesComboBox.Text = "";
			}
		}

		private void profilesComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (File.Exists(CoreData.profilePath + "\\profile_" + profilesComboBox.Text + ".txt"))
			{
				ActivateProfile(profilesComboBox.Text);
				profileNameUsed = profilesComboBox.Text;
			}
		}

		private void listViewMod_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			int count = listViewMod.CheckedItems.Count;
			count = ((e.NewValue != CheckState.Checked || e.CurrentValue != CheckState.Unchecked) ? (count - 1) : (count + 1));
			labelModCountNum.Text = count.ToString();
		}

		private void listViewMod_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			if (e.Column == lvwColumnSorter.SortColumn)
			{
				if (lvwColumnSorter.Order == SortOrder.Ascending)
				{
					lvwColumnSorter.Order = SortOrder.Descending;
				}
				else
				{
					lvwColumnSorter.Order = SortOrder.Ascending;
				}
			}
			else
			{
				lvwColumnSorter.SortColumn = e.Column;
				lvwColumnSorter.Order = SortOrder.Ascending;
			}
			listViewMod.Sort();
		}

		private void restoreFromArchiveToolStripMenuItem_MouseEnter(object sender, EventArgs e)
		{
			try
			{
				if (listViewMod.SelectedItems.Count <= 0)
				{
					return;
				}
				ListViewItem listViewItem = listViewMod.SelectedItems[0];
				if (listViewItem == null)
				{
					return;
				}
				int num = -1;
				for (int i = 0; i < modPacks.Count; i++)
				{
					if (modPacks[i].Name == listViewItem.Text)
					{
						num = i;
						break;
					}
				}
				if (num > -1)
				{
					string text = "Version dated: " + File.GetCreationTime(modPacks[num].FilePath).ToString();
					versionDatedXToolStripMenuItem.Text = text;
				}
			}
			catch
			{
			}
		}

		private void versionDatedXToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (listViewMod.SelectedItems.Count <= 0)
				{
					return;
				}
				ListViewItem listViewItem = listViewMod.SelectedItems[0];
				if (listViewItem == null)
				{
					return;
				}
				int num = -1;
				for (int i = 0; i < modPacks.Count; i++)
				{
					if (modPacks[i].Name == listViewItem.Text)
					{
						num = i;
						break;
					}
				}
				if (num > -1)
				{
					string text = corePrefs.pathBackup + Path.GetFileName(modPacks[num].FilePath);
					FileInfo fileInfo = new FileInfo(text);
					new FileInfo(modPacks[num].FilePath);
					fileInfo.CopyTo(modPacks[num].FilePath, true);
					FileInfo fileInfo2 = new FileInfo(text.Replace(".pack", ".png"));
					if (fileInfo2.Exists)
					{
						fileInfo2.CopyTo(modPacks[num].FilePath.Replace(".pack", ".png"), true);
					}
				}
			}
			catch
			{
			}
		}

		private void openSteamPage3ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				ListViewItem listViewItem = listViewMod.SelectedItems[0];
				if (listViewItem == null)
				{
					return;
				}
				int num = -1;
				for (int i = 0; i < modPacks.Count; i++)
				{
					if (modPacks[i].Name == listViewItem.Text)
					{
						num = i;
						break;
					}
				}
				if (num > -1)
				{
					SteamData value = null;
					steamDictionary.TryGetValue(modPacks[num].Name, out value);
					Process.Start(value.SteamURL);
				}
			}
			catch
			{
			}
		}

		private void listViewMod_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				ListViewItem itemAt = listViewMod.GetItemAt(e.X, e.Y);
				for (int i = 0; i < modPacks.Count; i++)
				{
					if (!(modPacks[i].Name == itemAt.Text))
					{
						continue;
					}
					string filePath = modPacks[i].FilePath;
					filePath = filePath.Replace(".pack", ".png");
					if (File.Exists(filePath))
					{
						if (pictureBox1.Image != null)
						{
							pictureBox1.Image.Dispose();
						}
						Image image = null;
						try
						{
							using (Bitmap original = new Bitmap(filePath))
							{
								image = new Bitmap(original);
							}
						}
						catch
						{
						}
						pictureBox1.Image = image;
					}
					else
					{
						pictureBox1.Image = Resources.yield_sign;
					}
					break;
				}
				if (!bypassConflictDetect && itemAt != null)
				{
					ListViewItem.ListViewSubItem subItemAt = itemAt.GetSubItemAt(e.X, e.Y);
					if (subItemAt == itemAt.SubItems[1] && subItemAt.Text.Length > 0 && subItemAt.Text != "None")
					{
						int num = -1;
						for (int j = 0; j < modPacks.Count; j++)
						{
							if (modPacks[j].Name == itemAt.Text)
							{
								num = j;
								break;
							}
						}
						if (num > -1)
						{
							FormConflict formConflict = new FormConflict(modPacks[num].Conflicts);
							formConflict.Text = itemAt.Text + " Conflicts";
							formConflict.ShowDialog();
						}
					}
				}
				itemAt.Selected = true;
				return;
			}
			ListViewItem listViewItem = listViewMod.SelectedItems[0];
			if (listViewItem == null)
			{
				return;
			}
			int num2 = -1;
			for (int k = 0; k < modPacks.Count; k++)
			{
				if (modPacks[k].Name == listViewItem.Text)
				{
					num2 = k;
					break;
				}
			}
			if (num2 > -1)
			{
				contextMenuStrip1.Show(Cursor.Position);
			}
		}

		private void showContentsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				ListViewItem listViewItem = listViewMod.SelectedItems[0];
				if (listViewItem == null)
				{
					return;
				}
				int num = -1;
				for (int i = 0; i < modPacks.Count; i++)
				{
					if (modPacks[i].Name == listViewItem.Text)
					{
						num = i;
						break;
					}
				}
				if (num <= -1)
				{
					return;
				}
				Form form = new Form();
				form.Icon = Icon;
				form.Width = 800;
				form.Height = 800;
				form.Text = "File list for " + listViewItem.Text;
				form.StartPosition = FormStartPosition.CenterParent;
				form.WindowState = FormWindowState.Normal;
				DataGridView dataGridView = new DataGridView();
				dataGridView.Columns.Add("filepathColumn", "File");
				dataGridView.Dock = DockStyle.Fill;
				dataGridView.AllowUserToAddRows = false;
				dataGridView.AllowUserToDeleteRows = false;
				dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
				dataGridView.RowHeadersVisible = false;
				form.Controls.Add(dataGridView);
				foreach (string file in modPacks[num].FileList)
				{
					dataGridView.Rows.Add(file);
				}
				form.Show();
			}
			catch
			{
			}
		}

		private void openWithPFMToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (listViewMod.SelectedItems.Count <= 0)
				{
					return;
				}
				ListViewItem listViewItem = listViewMod.SelectedItems[0];
				if (listViewItem == null)
				{
					return;
				}
				int num = -1;
				for (int i = 0; i < modPacks.Count; i++)
				{
					if (modPacks[i].Name == listViewItem.Text)
					{
						num = i;
						break;
					}
				}
				if (num <= -1)
				{
					return;
				}
				if (corePrefs.pathPFM != null && File.Exists(corePrefs.pathPFM))
				{
					Process process = new Process();
					process.StartInfo.Arguments = "\"" + modPacks[num].FilePath + "\"";
					process.StartInfo.FileName = corePrefs.pathPFM;
					process.Start();
					return;
				}
				openPackFileDialog.Title = "Please select the folder where you have PackFileManager.exe located for the Pack File Manager.";
				openPackFileDialog.FileName = "PackFileManager.exe";
				if (openPackFileDialog.ShowDialog() == DialogResult.OK)
				{
					corePrefs.pathPFM = openPackFileDialog.FileName;
				}
			}
			catch
			{
			}
		}

		private void openWithRPFMToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (listViewMod.SelectedItems.Count <= 0)
				{
					return;
				}
				ListViewItem listViewItem = listViewMod.SelectedItems[0];
				if (listViewItem == null)
				{
					return;
				}
				int num = -1;
				for (int i = 0; i < modPacks.Count; i++)
				{
					if (modPacks[i].Name == listViewItem.Text)
					{
						num = i;
						break;
					}
				}
				if (num <= -1)
				{
					return;
				}
				if (corePrefs.pathRPFM != null && File.Exists(corePrefs.pathRPFM))
				{
					Process process = new Process();
					process.StartInfo.Arguments = "\"" + modPacks[num].FilePath + "\"";
					process.StartInfo.FileName = corePrefs.pathRPFM;
					process.Start();
					return;
				}
				openPackFileDialog.Title = "Please select the folder where you have rpfm-code.exe located for the Rusted Pack File Manager.";
				openPackFileDialog.FileName = "rpfm-code.exe";
				if (openPackFileDialog.ShowDialog() == DialogResult.OK)
				{
					corePrefs.pathRPFM = openPackFileDialog.FileName;
				}
			}
			catch
			{
			}
		}

		private void saveToArchiveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (listViewMod.SelectedItems.Count <= 0)
				{
					return;
				}
				ListViewItem listViewItem = listViewMod.SelectedItems[0];
				if (listViewItem == null)
				{
					return;
				}
				int num = -1;
				for (int i = 0; i < modPacks.Count; i++)
				{
					if (modPacks[i].Name == listViewItem.Text)
					{
						num = i;
						break;
					}
				}
				if (num > -1)
				{
					string text = corePrefs.pathBackup + Path.GetFileName(modPacks[num].FilePath);
					new FileInfo(modPacks[num].FilePath).CopyTo(text, true);
					FileInfo fileInfo = new FileInfo(modPacks[num].FilePath.Replace(".pack", ".png"));
					if (fileInfo.Exists)
					{
						fileInfo.CopyTo(text.Replace(".pack", ".png"), true);
					}
				}
			}
			catch
			{
			}
		}

		private void deleteDataToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				ListViewItem listViewItem = listViewMod.SelectedItems[0];
				if (listViewItem == null)
				{
					return;
				}
				int num = -1;
				for (int i = 0; i < modPacks.Count; i++)
				{
					if (modPacks[i].Name == listViewItem.Text)
					{
						num = i;
						break;
					}
				}
				if (num > -1)
				{
					string path = modPacks[num].FilePath.Replace(".pack", ".png");
					File.Delete(modPacks[num].FilePath);
					if (File.Exists(path))
					{
						File.Delete(path);
					}
					RefreshApp();
				}
			}
			catch
			{
			}
		}

		private void copyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				ListViewItem listViewItem = listViewMod.SelectedItems[0];
				if (listViewItem == null)
				{
					return;
				}
				int num = -1;
				for (int i = 0; i < modPacks.Count; i++)
				{
					if (modPacks[i].Name == listViewItem.Text)
					{
						num = i;
						break;
					}
				}
				if (num > -1)
				{
					string text = modPacks[num].FilePath.Replace(".pack", "_copy.pack");
					File.Copy(modPacks[num].FilePath, text);
					string text2 = modPacks[num].FilePath.Replace(".pack", ".png");
					string destFileName = text.Replace(".pack", ".png");
					if (File.Exists(text2))
					{
						File.Copy(text2, destFileName);
					}
					RefreshApp();
				}
			}
			catch
			{
			}
		}

		private void renameToolStripMenuItem_MouseEnter(object sender, EventArgs e)
		{
			try
			{
				if (listViewMod.SelectedItems.Count <= 0)
				{
					return;
				}
				ListViewItem listViewItem = listViewMod.SelectedItems[0];
				if (listViewItem == null)
				{
					return;
				}
				int num = -1;
				for (int i = 0; i < modPacks.Count; i++)
				{
					if (modPacks[i].Name == listViewItem.Text)
					{
						num = i;
						break;
					}
				}
				if (num > -1)
				{
					toolStripTextBox1.Width = modPacks[num].Name.Length * 7;
					toolStripTextBox1.Text = modPacks[num].Name;
				}
			}
			catch
			{
			}
		}

		private void toolStripTextBox1_MouseLeave(object sender, EventArgs e)
		{
			try
			{
				ListViewItem listViewItem = listViewMod.SelectedItems[0];
				if (listViewItem == null)
				{
					return;
				}
				int num = -1;
				for (int i = 0; i < modPacks.Count; i++)
				{
					if (modPacks[i].Name == listViewItem.Text)
					{
						num = i;
						break;
					}
				}
				if (num > -1)
				{
					string text = modPacks[num].FilePath.Replace(modPacks[num].Name, toolStripTextBox1.Text);
					string sourceFileName = modPacks[num].FilePath.Replace(".pack", ".png");
					string destFileName = text.Replace(".pack", ".png");
					File.Move(modPacks[num].FilePath, text);
					File.Move(sourceFileName, destFileName);
					RefreshApp();
				}
			}
			catch
			{
			}
		}

		private void openSteamPage2ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				ListViewItem listViewItem = listViewMod.SelectedItems[0];
				if (listViewItem == null)
				{
					return;
				}
				int num = -1;
				for (int i = 0; i < modPacks.Count; i++)
				{
					if (modPacks[i].Name == listViewItem.Text)
					{
						num = i;
						break;
					}
				}
				if (num > -1)
				{
					SteamData value = null;
					steamDictionary.TryGetValue(modPacks[num].Name, out value);
					Process.Start("steam://url/CommunityFilePage/" + value.SteamID);
				}
			}
			catch
			{
			}
		}
	}
}
