using AVBC_CLCB_Notifier.PL.CustomControls.CustomControlsRepos;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AVBC_CLCB_Notifier.PL.CustomControls
{
    public partial class CustomDataGridView : CustomControl
    {
        private List<object> DataSource = new(); //Lista de objetos que formada pelos 
        private List<PropertyInfo> Properties = new();
        private List<InnerLabel> HeaderLabels = new();

        private InnerLabel[,]? DataLabels = null;
        private ColumnWidthModeEnum columnWidthMode = ColumnWidthModeEnum.HeaderWidth;

        private int RowHoveredIndex;
        private int columnHoveredIndex;
        private int fixedCharCount = 10;
        private int linesWidth = 1;

        private bool linesBetweenColumns;
        private bool linesBetweenRows;
        private bool differentColorsBetweenRows;
        public int LinesWidth
        {
            get => linesWidth;
            set { linesWidth = value; Invalidate();}
        }
        public bool DifferentColorsBetweenRows
        {
            get => differentColorsBetweenRows;
            set { differentColorsBetweenRows = value; Invalidate(); }
        }
        public bool LinesBetweenRows
        {
            get => linesBetweenRows;
            set { linesBetweenRows = value; Invalidate(); }
        }
        public bool LinesBetweenColumns
        {
            get => linesBetweenColumns;
            set{ linesBetweenColumns = value; Invalidate(); }
        }
        public ColumnWidthModeEnum ColumnWidthMode
        {
            get => columnWidthMode;
            set { columnWidthMode = value; Invalidate(); }
        }
        public int FixedCharCount
        {
            get => fixedCharCount;
            set { fixedCharCount = value; Invalidate(); }
        }
        public enum ColumnWidthModeEnum
        {
            HeaderWidth,
            FixedCharWidth
        }
        public CustomDataGridView()
        {
        }
        public void SetDataSource<T>(List<T> _source)
        {
            if (_source == null || _source.Count == 0) //Se não a fonte de dados(list) for null ou não tiver 
                return;

            // Armazena os dados internamente como uma lista de objetos
            DataSource = _source.Cast<object>().ToList(); //Passa os elementos de source para o 

            // Descobre as propriedades públicas da classe T
            Properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                  .Where(p => p.CanRead)
                                  .ToList();

            // Solicita o redesenho do controle
            CreateHeadersLabels();
            CreateDataLabels();
            AdjustControlSize();
            Invalidate();
        }
        public void CreateHeadersLabels()
        {
            foreach (var header in HeaderLabels)
                this.InnerControls.Remove(header);

            HeaderLabels.Clear();

            for (int i = 0; i < Properties.Count; i++)
            {
                InnerLabel columnHeader = new InnerLabel()
                {
                    //Name = $"Item{Properties[i].Name}",
                    Text = Properties[i].Name,
                    Font = Font,
                    BackgroundColor = HeaderBackgroundColor,
                    TextHorizontalPadding = true
                };

                this.InnerControls.Add(columnHeader);
                HeaderLabels.Add(columnHeader);
            }
        }
        public void CreateDataLabels()
        {
            if (DataSource == null || Properties == null) return;

            int rows = DataSource.Count;
            int cols = Properties.Count;

            DataLabels = new InnerLabel[rows, cols];

            for (int rowIndex = 0; rowIndex < rows; rowIndex++)
            {
                for (int colIndex = 0; colIndex < cols; colIndex++)
                {
                    object val = Properties[colIndex].GetValue(DataSource[rowIndex]) ?? "";
                    string text = val.ToString();

                    InnerLabel InnerLabel = new InnerLabel()
                    {
                        Text = text,
                        Font = Font,
                        ForeColor = ForeColor,
                        BackgroundColor = DifferentColorsBetweenRows && rowIndex % 2 == 1 ? SecondaryBackgroundColor : BackgroundColor,
                        TextHorizontalPadding = true
                    };
                    InnerLabel.Click += (s, e) => MessageBox.Show(InnerLabel.Text);
                    this.InnerControls.Add(InnerLabel);

                    DataLabels[rowIndex, colIndex] = InnerLabel;
                }
            }
        }

        protected override void AdjustControlSize()
        {
            base.AdjustControlSize();
            if (DataLabels == null || HeaderLabels == null) return;

            int xPadding = HorizontalPadding;
            int yPadding = VerticalPadding;

            int rows = DataLabels.GetLength(0); // Linhas (dados)
            int cols = DataLabels.GetLength(1); // Colunas (propriedades)

            int currentX = BorderWidth;
            
            for (int columnIndex = 0; columnIndex < cols; columnIndex++)
            {

                var header = HeaderLabels[columnIndex]; int columnWidth = ColumnWidth(ColumnWidthMode, header.Width, xPadding);
                header.Location = new Point(currentX, BorderWidth);
                header.Width = columnWidth;
                header.Height = header.Height + yPadding;
                header.RefreshLayout();
                int currentY = header.Height;
                
                for (int rowIndex = 0; rowIndex < rows; rowIndex++)
                {
                    var cell = DataLabels[rowIndex, columnIndex];
                    cell.Location = new Point(currentX, currentY);
                    cell.Width = columnWidth;
                    cell.Height = cell.Height + yPadding;
                    cell.RefreshLayout();
                    currentY = LinesBetweenRows ? currentY + (cell.Height + LinesWidth) : currentY + cell.Height;
                }
                currentX = LinesBetweenColumns ? currentX + (header.Width + LinesWidth): currentX + header.Width;
            }
        }

        public int ColumnWidth(ColumnWidthModeEnum columnWidthMode, int headerWidth, int xPadding)
        {
            int columnWidth = 0;

            if (columnWidthMode == ColumnWidthModeEnum.HeaderWidth)
                columnWidth = headerWidth + xPadding;

            if (columnWidthMode == ColumnWidthModeEnum.FixedCharWidth)
            {
                string sample = new string('0', FixedCharCount);
                columnWidth = NhegazSizeMethods.textExactSize(sample, Font).Width + xPadding;
            }

            return columnWidth;
        }
        protected override void OnPaint(PaintEventArgs e)
        {            
            base.OnPaint(e);           
            
            if (DataLabels == null || HeaderLabels == null)
                return;

            if (LinesBetweenColumns)
            {
                int col = HeaderLabels.Count - 1;
                Pen pen = new(BorderColor, LinesWidth);
                for (int i = 0; i < col; i++)
                {
                    int locX = HeaderLabels[i].Location.X + HeaderLabels[i].Width;
                    e.Graphics.DrawLine(pen, new Point(locX, BorderWidth), new Point(locX, Bottom - BorderWidth));
                }
            }

            if (LinesBetweenRows) 
            {
                int row = DataLabels.GetLength(0);
                Pen pen = new(BorderColor, LinesWidth);
                for (int i = 0; i < row; i++)
                {
                    int locY = DataLabels[i, 0].Location.Y;// + DataLabels[i,0].Height;
                    e.Graphics.DrawLine(pen, new Point(BorderWidth, locY), new Point(Right, locY));
                }
            }

            
        }

    }
}
