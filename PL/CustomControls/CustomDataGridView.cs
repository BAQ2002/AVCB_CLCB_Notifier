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
    public class CustomDataGridView : CustomControl
    {
        private List<object> DataSource = new(); //Lista de objetos que formada pelos 
        private List<PropertyInfo> Properties = new();
        private List<CustomLabel> ColumnsHeadersList = new();

        private InnerLabel[,] DataVirtualLabels;
        private ColumnWidthModeEnum columnWidthMode = ColumnWidthModeEnum.HeaderWidth;

        private int RowHoveredIndex;
        private int columnHoveredIndex;
        private int fixedCharCount = 10;

        private bool LinesBetweenColumns;
        private bool LinesBetweenRows;
        private bool differentColorsBetweenRows;

        public bool DifferentColorsBetweenRows
        {
            get => differentColorsBetweenRows;
            set { differentColorsBetweenRows = value; Invalidate(); }
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
            BiggestContentWidth,
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
            CreateColumnsHeaders();
            CreateVirtualCells();
            AdjustControlSize();
            Invalidate();
        }
        public void CreateColumnsHeaders()
        {
            foreach (var header in ColumnsHeadersList)
                this.Controls.Remove(header);

            ColumnsHeadersList.Clear();

            for (int i = 0; i < Properties.Count; i++)
            {
                CustomLabel columnHeader = new CustomLabel()
                {
                    Name = $"Item{Properties[i].Name}",
                    Text = Properties[i].Name,
                    Font = Font,
                    BackgroundColor = BackgroundColor
                };

                this.Controls.Add(columnHeader);
                ColumnsHeadersList.Add(columnHeader);
            }
        }
        public void CreateVirtualCells()
        {
            if (DataSource == null || Properties == null) return;

            int rows = DataSource.Count;
            int cols = Properties.Count;

            DataVirtualLabels = new InnerLabel[rows, cols];

            for (int rowIndex = 0; rowIndex < rows; rowIndex++)
            {
                for (int colIndex = 0; colIndex < cols; colIndex++)
                {
                    object val = Properties[colIndex].GetValue(DataSource[rowIndex]) ?? "";
                    string text = val.ToString();

                    var InnerLabel = new InnerLabel(this, text, 
                                                               DifferentColorsBetweenRows && rowIndex % 2 == 1 ? SecondaryBackgroundColor : BackgroundColor);

                    InnerLabel.Click += (s, e) => MessageBox.Show(InnerLabel.Text);
                    this.InnerControls.Add(InnerLabel);
                    
                    DataVirtualLabels[rowIndex, colIndex] = InnerLabel;
                }
            }
        }

        protected override void AdjustControlSize()
        {
            base.AdjustControlSize();

            int xPadding = HorizontalPadding; 
            int yPadding = VerticalPadding;

            int rows = DataVirtualLabels.GetLength(0); // Linhas (dados)
            int cols = DataVirtualLabels.GetLength(1); // Colunas (propriedades)

            int currentX = xPadding;

            for (int col = 0; col < cols; col++)
            {
                var header = ColumnsHeadersList[col];  
                int columnWidth = ColumnWidth(ColumnWidthMode, header.Width, xPadding);
                header.Location = new Point(currentX, BorderWidth);
                header.Width = columnWidth;
                header.Height = header.Height + yPadding;
 
                int currentY = header.Height + yPadding;
                
                for (int row = 0; row < rows; row++)
                {
                    var cell = DataVirtualLabels[row, col];
                    cell.Location = new Point(currentX, currentY);
                    cell.Width = columnWidth;
                    cell.Height = cell.Height + yPadding;

                    currentY += cell.Height;
                }

                currentX += header.Width;
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
            
            if (DataVirtualLabels == null)
                return;
        }

    }
}
