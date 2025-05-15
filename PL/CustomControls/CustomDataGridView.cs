using AVBC_CLCB_Notifier.PL.CustomControls.CustomControlsRepos;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AVBC_CLCB_Notifier.PL.CustomControls
{
    public class CustomDataGridView : CustomControl
    {
        private List<object> DataSource = new();
        private List<PropertyInfo> Properties = new();
        private InnerLabel[,] DataLabels; //Matriz[ linha, coluna]
        private List<InnerLabel> ColumnsHeadersList = new();   
        
        private int RowHoveredIndex;
        private int columnHoveredIndex;
        private bool LinesBetweenColumns;
        private bool LinesBetweenRows;
        private bool DiferentColorsBetweenRows;
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
            CreateCells();
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
                InnerLabel columnHeader = new InnerLabel()
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
        public void CreateCells()
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

                    var label = new InnerLabel
                    {
                        Name = $"Cell_{rowIndex}_{colIndex}",
                        Text = text,
                        Font = Font,
                        ForeColor = ForeColor,
                        BackgroundColor = BackgroundColor
                    };

                    this.Controls.Add(label);
                    DataLabels[rowIndex, colIndex] = label;
                }
            }
        }
        public void AdjustControlSize()
        {
            AdjustPadding();

            int xPadding = HorizontalPadding;
            int yPadding = VerticalPadding;

            int rows = DataLabels.GetLength(0); // Linhas (dados)
            int cols = DataLabels.GetLength(1); // Colunas (propriedades)

            int currentX = xPadding;
            int headerHeight = 0;

            // Primeiro loop: colunas (cabeçalhos e preparação para colunas de células)
            for (int col = 0; col < cols; col++)
            {
                var header = ColumnsHeadersList[col];
                header.Location = new Point(currentX, yPadding);
                headerHeight = Math.Max(headerHeight, header.Height);

                int currentY = yPadding + header.Height + yPadding;

                // Segundo loop: linhas (células de dados)
                for (int row = 0; row < rows; row++)
                {
                    var cell = DataLabels[row, col];
                    cell.Location = new Point(currentX, currentY);
                    currentY += cell.Height + yPadding;
                }

                currentX += header.Width + xPadding;
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            NhegazDrawingMethods.DrawControl(this, e);
        }

    }
}
