using Abeslamidze_Kursovaya7.Models;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Abeslamidze_Kursovaya7.ViewModels
{
    public abstract class BaseDiagramWindowViewModel
    {
        public BaseDiagramWindowViewModel(List<double> plotValues)
        {
            PlotModel = new PlotModel
            {
                Title = Title,
            };

            // specify key and position
            var valueAxis = new LinearAxis
            {
                Position = AxisPosition.Left,
                Key = "Value",
                Title= LeftTitle,
            };
            PlotModel.Axes.Add(valueAxis);

            // specify axis keys
            var barSeries = new BarSeries { XAxisKey = "Value", YAxisKey = "Category" };
            for (int i = 0; i < 12; i++) // try get first 12 items
            {
                barSeries.Items.Add(new BarItem
                {
                    CategoryIndex = i,
                    Value = plotValues[i],
                });
            }
            PlotModel.Series.Add(barSeries);


            // specify key and position
            var categoryAxis = new CategoryAxis
            {
                Position = AxisPosition.Bottom,
                Key = "Category",
                Title = "Месяца"
            };
            categoryAxis.Labels.Add("Январь");
            categoryAxis.Labels.Add("Февраль");
            categoryAxis.Labels.Add("Март");
            categoryAxis.Labels.Add("Апрель");
            categoryAxis.Labels.Add("Май");
            categoryAxis.Labels.Add("Июнь");
            categoryAxis.Labels.Add("Июль");
            categoryAxis.Labels.Add("Август");
            categoryAxis.Labels.Add("Сентябрь");
            categoryAxis.Labels.Add("Октябрь");
            categoryAxis.Labels.Add("Ноябрь");
            categoryAxis.Labels.Add("Декабрь");
            PlotModel.Axes.Add(categoryAxis);
        }

        public PlotModel PlotModel { get; private set; }

        public abstract string Title { get; }
        public abstract string LeftTitle { get; }
    }

    public class DeliveryCountDiagramViewModel : BaseDiagramWindowViewModel
    {
        public DeliveryCountDiagramViewModel(List<Delivery> data) : base(CalculatePlotValues(data))
        {
        }

        public override string Title { get; } = "Кол-во грузоперевозок по месяцам";
        public override string LeftTitle { get; } = "Кол-во";

        private static List<double> CalculatePlotValues(List<Delivery> data)
        {
            var currentYear = DateTime.Now.Year;

            var buffer = new List<double>(new double[12]);
            var result = data
                .Where(d => d.StartDate.HasValue && d.StartDate!.Value.Year == currentYear)
                .GroupBy(d => d.StartDate!.Value.Month, (key, group) => new
                {
                    month = key,
                    count = group.Count(),
                });

            foreach (var item in result)
            {
                buffer[item.month - 1] = item.count;
            }

            return buffer;
        }
    }


    public class DeliveryTotalPriceDiagramViewModel : BaseDiagramWindowViewModel
    {
        public DeliveryTotalPriceDiagramViewModel(List<Delivery> data) : base(CalculatePlotValues(data))
        {
        }

        public override string Title { get; } = "Объем грузоперевозок по месяцам";
        public override string LeftTitle { get; } = "Объем (в рублях)";

        private static List<double> CalculatePlotValues(List<Delivery> data)
        {
            var currentYear = DateTime.Now.Year;

            var buffer = new List<double>(new double[12]);
            var result = data
                .Where(d => d.StartDate.HasValue && d.StartDate!.Value.Year == currentYear)
                .GroupBy(d => d.StartDate!.Value.Month, (key, group) => new
                { 
                    month = key,
                    sum = group.Sum(d => d.Price ?? 0)
                });

            foreach (var item in result)
            {
                buffer[item.month - 1] = item.sum;
            }

            return buffer;
        }
    }
}
