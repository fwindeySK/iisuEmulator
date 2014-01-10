using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace iisuEmulator
{
    public partial class EmulatorView : Form, IEmulatorView
    {
        private Image _imagePlaying;
        private Image _imageStopped;

        private int _previousHeight;
        private int _previousWidth;

        private int _editedRowIndex;

        #region IEmulatorView Members

        public event EventHandler<EventArgs> PlayPressed;
        public event EventHandler<OpenIIDProjectEventArgs> IIDProjectOpened;
        public event EventHandler<ChangedMappingEventArgs> MappingChanged;
        public event EventHandler<ChangedMappingEventArgs> EmulatorChanged;
        public event EventHandler<ChangedStartStopMappingEventArgs> StartStopMappingChanged;
        public event EventHandler<ChangedMappingEnabledEventArgs> MappingEnabledChanged;
        public event EventHandler<SaveEventArgs> SaveProject;
        public event EventHandler<LoadEventArgs> LoadProject;
        public event EventHandler<EventArgs> NewProject;
        public event EventHandler<EventArgs> LaunchToolBox;
        public event EventHandler<EventArgs> Quit;
        public event EventHandler<OpenIIDProjectEventArgs> OpenIIDProject;

        #endregion

        public EmulatorView()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            System.Reflection.Assembly myAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            Stream myStream = myAssembly.GetManifestResourceStream("iisuEmulator.icons.ArrowPlaying.png");
            _imagePlaying = Image.FromStream(myStream);

            myStream = myAssembly.GetManifestResourceStream("iisuEmulator.icons.ArrowStopped.png");
            _imageStopped = Image.FromStream(myStream);

            dgMappings.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dgMappings_EditingControlShowing);

            _previousHeight = Height;
            _previousWidth = Width;

            bToolBox.Enabled = false;
        }

        //http://msdn.microsoft.com/en-us/library/system.windows.forms.datagridviewcomboboxeditingcontrol.aspx
        void dgMappings_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView dg = (DataGridView)sender;

            ComboBox combo = e.Control as ComboBox;
            if (combo != null)
            {
                combo.SelectedIndexChanged -=
                    new EventHandler(ComboBox_SelectedIndexChanged);

                combo.SelectedIndexChanged +=
                    new EventHandler(ComboBox_SelectedIndexChanged);
            }
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _editedRowIndex = dgMappings.SelectedCells[0].OwningRow.Index;

            ComboBox combo = (ComboBox)sender;

            string emulator = "";
            string datasource = "";
            string iidPath = ((DataGridViewTextBoxCell)dgMappings[1, _editedRowIndex]).Value.ToString();

            if (dgMappings.CurrentCell.ColumnIndex == 3)
            {
                if (combo.SelectedItem != null)
                    emulator = combo.SelectedItem.ToString();
                else
                    emulator = ((DataGridViewComboBoxCell)dgMappings[3, _editedRowIndex]).Value.ToString();

                datasource = ((DataGridViewComboBoxCell)dgMappings[4, _editedRowIndex]).Value.ToString();

                if (EmulatorChanged != null)
                {
                    EmulatorChanged(this, new ChangedMappingEventArgs(iidPath, emulator, datasource, dgMappings[2, _editedRowIndex].Value.ToString(), _editedRowIndex));
                }

            }
            else if (dgMappings.CurrentCell.ColumnIndex == 4)
            {
                emulator = ((DataGridViewComboBoxCell)dgMappings[3, _editedRowIndex]).Value.ToString();

                if (combo.SelectedItem != null)
                    datasource = combo.SelectedItem.ToString();
                else
                    datasource = ((DataGridViewComboBoxCell)dgMappings[4, _editedRowIndex]).Value.ToString();

                if (MappingChanged != null)
                {
                    MappingChanged(this, new ChangedMappingEventArgs(iidPath, emulator, datasource, dgMappings[2, _editedRowIndex].Value.ToString(), _editedRowIndex));
                }
            }

            
        }

        private void cbPlay_CheckedChanged(object sender, EventArgs e)
        {
            if (PlayPressed != null)
            {
                PlayPressed(this, new EventArgs());
            }
        }

        #region IEmulatorView Members

        public void SetPlayingIcon(bool playing)
        {
            if (playing)
            {
                cbPlay.Image = _imagePlaying;
            }
            else
            {
                cbPlay.Image = _imageStopped;
            }
        }

        public void ClearMappings()
        {
            dgMappings.Rows.Clear();
        }

        public void AddMapping(IMapping mapping, string[] comboBoxItems, int defaultValueIndex, MappingType type)
        {
            DataGridViewRow row = new DataGridViewRow();

            DataGridViewCheckBoxCell dgvCheck = new DataGridViewCheckBoxCell();
            dgvCheck.TrueValue = 1;
            dgvCheck.FalseValue = 0;
            dgvCheck.Value = 1;
            row.Cells.Add(dgvCheck);

            DataGridViewTextBoxCell tbIIDOutput = new DataGridViewTextBoxCell();
            tbIIDOutput.Value = mapping.IIDOutputPath;
            row.Cells.Add(tbIIDOutput);

            DataGridViewTextBoxCell tbType = new DataGridViewTextBoxCell();
            tbType.Value = Enum.GetName(typeof(MappingType), type);
            row.Cells.Add(tbType);

            DataGridViewComboBoxCell cbCell = new DataGridViewComboBoxCell();
            cbCell.FlatStyle = FlatStyle.Flat;            
            cbCell.Items.AddRange(comboBoxItems);
            cbCell.Value = comboBoxItems[defaultValueIndex];
            row.Cells.Add(cbCell);

            DataGridViewComboBoxCell cbCell2 = new DataGridViewComboBoxCell();
            cbCell2.FlatStyle = FlatStyle.Flat;
            cbCell2.Items.Add("NONE");
            cbCell2.Value = "NONE";
            row.Cells.Add(cbCell2);

            dgMappings.Rows.Add(row);
        }

        public void AddStartStopMapping(string item)
        {
            cbStartStop.Items.Add(item);
            if (cbStartStop.Items.Count == 1)
            {
                cbStartStop.SelectedIndex = 0;
            }
        }

        public void ClearStartStopMapping()
        {
            cbStartStop.Items.Clear();
        }

        public void UpdateDataSourceItems(int row, string[] dataSourceItems)
        {
            DataGridViewComboBoxCell cell = (DataGridViewComboBoxCell)dgMappings[4, row];
            cell.Items.Clear();
            cell.Items.AddRange(dataSourceItems);
            cell.Value = dataSourceItems[0];
        }

        public void SetIIDProjectPath(string path)
        {
            tbIIDProject.Text = path;
        }

        public void SetStartStopMapping(string iidOutputPath)
        {
            for (int i = 0; i < cbStartStop.Items.Count; ++i)
            {
                if ((string)cbStartStop.Items[i] == iidOutputPath)
                {
                    cbStartStop.SelectedIndex = i;
                    break;
                }
            }
        }

        public void UpdateMappings(IMapping mapping, string[] dataSourceItems, string dataSource)
        {
            int mappingIndex = 0;
            for (mappingIndex = 0; mappingIndex < dgMappings.Rows.Count; ++mappingIndex)
            {
                if(dgMappings[1,mappingIndex].Value.ToString() == mapping.IIDOutputPath)
                    break;
            }
            if (mapping.Emulator != null)
            {
                dgMappings[3, mappingIndex].Value = mapping.Emulator.Name;
                DataGridViewComboBoxCell cell = (DataGridViewComboBoxCell)dgMappings[4, mappingIndex];
                cell.Items.Clear();
                cell.Items.AddRange(dataSourceItems);
                cell.Value = dataSource;
            }
            else
            {
                dgMappings[3, mappingIndex].Value = "NONE";
                DataGridViewComboBoxCell cell = (DataGridViewComboBoxCell)dgMappings[4, mappingIndex];
                cell.Items.Clear();
                cell.Items.AddRange(dataSourceItems);
                cell.Value = dataSource;
            }
        }

        public string GetIIDProjectFile(string path)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "IID Project not found, please point to the right location of the IID project";
            dialog.FileName = Path.GetFileName(path);
            dialog.Filter = "Interaction designer project (*.iid)|*.iid";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return dialog.FileName;
            }
            else
            {
                return "";
            }
        }

        public void SetRuntimeCriticalControlsEnabled(bool enabled)
        {
            tbIIDProject.Enabled = enabled;
            bNew.Enabled = enabled;
            bOpen.Enabled = enabled;
            bBrowseIID.Enabled = enabled;
            bToolBox.Enabled = !enabled;
        }

        public void ShowPopUp(string message)
        {
            MessageBox.Show(message);
        }

        #endregion

        private void bBrowseIID_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Interaction designer project (*.iid)|*.iid";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (IIDProjectOpened != null)
                {
                    tbIIDProject.Text = dialog.FileName;
                    IIDProjectOpened(this, new OpenIIDProjectEventArgs(dialog.FileName));
                }
            }
        }

        private void EmulatorView_SizeChanged(object sender, EventArgs e)
        {
            dgMappings.Height += (Height - _previousHeight);
            _previousHeight = Height;
            dgMappings.Width += (Width - _previousWidth);
            skLogo.Left += (Width - _previousWidth);
            _previousWidth = Width;
        }

        private void cbStartStop_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (StartStopMappingChanged != null)
            {
                StartStopMappingChanged(this, new ChangedStartStopMappingEventArgs(((ComboBox)sender).SelectedItem.ToString()));
            }
        }

        //http://msdn.microsoft.com/en-us/library/system.windows.forms.datagridview.currentcelldirtystatechanged.aspx
        private void dgMappings_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgMappings.IsCurrentCellDirty)
            {
                dgMappings.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        public void dgMappings_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                if (MappingEnabledChanged != null)
                {
                    MappingEnabledChanged(this, new ChangedMappingEnabledEventArgs((int)dgMappings[e.ColumnIndex, e.RowIndex].Value == 0? false : true, dgMappings[1, e.RowIndex].Value.ToString()));
                }
            }
        }

        private void bToolBox_Click(object sender, EventArgs e)
        {
            if (LaunchToolBox != null)
            {
                LaunchToolBox(this, new EventArgs());
            }
        }

        private void EmulatorView_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Quit != null)
            {
                Quit(this, new EventArgs());
            }
        }

        private void bOpenIIDProject_Click(object sender, EventArgs e)
        {
            if (OpenIIDProject != null)
            {
                OpenIIDProject(this, new OpenIIDProjectEventArgs(tbIIDProject.Text));
            }
        }

        private void bNew_Click(object sender, EventArgs e)
        {
            if (NewProject != null)
            {
                NewProject(this, new EventArgs());
            }
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Title = "Save iisu Emulator Project";
            saveDialog.Filter = "iisu Emulator Project | *.iim";
            saveDialog.OverwritePrompt = true;

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                if (SaveProject != null)
                {
                    SaveProject(this, new SaveEventArgs(tbIIDProject.Text, saveDialog.FileName));
                }
            }
        }

        private void bOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Open iisu Emulator Project";
            openFileDialog.Filter = "iisu Emulator Project | *.iim";
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (LoadProject != null)
                {
                    LoadProject(this, new LoadEventArgs(openFileDialog.FileName));
                }
            }
        }

        private void bClose_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
