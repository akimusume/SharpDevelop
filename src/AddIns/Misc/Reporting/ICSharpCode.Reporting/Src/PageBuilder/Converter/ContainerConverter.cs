﻿/*
 * Created by SharpDevelop.
 * User: Peter Forstmeier
 * Date: 08.04.2013
 * Time: 19:49
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;

using ICSharpCode.Reporting.Factories;
using ICSharpCode.Reporting.Interfaces;
using ICSharpCode.Reporting.Interfaces.Export;
using ICSharpCode.Reporting.PageBuilder.ExportColumns;

namespace ICSharpCode.Reporting.PageBuilder.Converter
{
	/// <summary>
	/// Description of SectionConverter.
	/// </summary>
	internal class ContainerConverter : IContainerConverter
	{
		public ContainerConverter(Graphics graphics, Point currentLocation)
		{
			if (graphics == null) {
				throw new ArgumentNullException("graphics");
			}
			Graphics = graphics;
			CurrentLocation = currentLocation;
		}


		public virtual IExportContainer Convert(IReportContainer reportContainer)
		{
			var exportContainer = (ExportContainer)reportContainer.CreateExportColumn();
			exportContainer.Location = CurrentLocation;
			exportContainer.DesiredSize = Measure(exportContainer);
			return exportContainer;
		}

		
		public List<IExportColumn> CreateConvertedList(IReportContainer reportContainer,IExportContainer exportContainer,Point position){

			var itemsList = new List<IExportColumn>();
			foreach (var element in reportContainer.Items) {
				var exportColumn = ExportColumnFactory.CreateItem(element);
				exportColumn.Parent = exportContainer;
				exportColumn.Location = new Point(element.Location.X,element.Location.Y + position.Y);
//				exportColumn.Location = new Point(element.Location.X,element.Location.Y + position.Y);
				itemsList.Add(exportColumn);
			}
			return itemsList;
		}

		public List<IExportColumn> CreateConvertedList(IReportContainer reportContainer,IExportContainer exportContainer)
		{
//			Console.WriteLine("CreateConvertedList {0}",reportContainer.Name);
			var itemsList = new List<IExportColumn>();
			foreach (var element in reportContainer.Items) {
				var exportColumn = ExportColumnFactory.CreateItem(element);
				exportColumn.Parent = exportContainer;
//				exportColumn.Location = new Point(element.Location.X,element.Location.Y + exportContainer.Location.Y);
//				exportColumn.Location = new Point(element.Location.X,element.Location.Y + position.Y);
//				exportColumn.DesiredSize = Measure(exportColumn);
				itemsList.Add(exportColumn);
			}
			return itemsList;
		}

		public Size Measure(IExportColumn element)
		{
//			Console.WriteLine("Measure {0}",element.Name);
			var measureStrategy = element.MeasurementStrategy();
			return measureStrategy.Measure(element, Graphics);
		}

		public void ArrangeContainer(IExportContainer exportContainer)
		{
//			Console.WriteLine("ArrangeContainer {0}",exportContainer.Name);
			var exportArrange = exportContainer.GetArrangeStrategy();
			exportArrange.Arrange(exportContainer);
		}
		
		
		internal IReportContainer Container { get; private set; }

		protected Point CurrentLocation { get;  set; }
		
		internal Graphics Graphics {get;private set;}
	}
}
