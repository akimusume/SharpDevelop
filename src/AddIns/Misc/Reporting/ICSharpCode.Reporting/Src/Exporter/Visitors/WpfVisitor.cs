﻿/*
 * Created by SharpDevelop.
 * User: Peter Forstmeier
 * Date: 06.05.2013
 * Time: 20:10
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows;
using ICSharpCode.Reporting.ExportRenderer;
using ICSharpCode.Reporting.Items;
using ICSharpCode.Reporting.PageBuilder.ExportColumns;

namespace ICSharpCode.Reporting.Exporter.Visitors
{
	/// <summary>
	/// Description of WpfVisitor.
	/// </summary>
	internal class WpfVisitor: AbstractVisitor
	{
		private readonly FixedDocumentCreator documentCreator;
		private readonly  ReportSettings reportSettings;
		
		public WpfVisitor(ReportSettings reportSettings)
		{
			if (reportSettings == null)
				throw new ArgumentNullException("reportSettings");
			this.reportSettings = reportSettings;
			documentCreator = new FixedDocumentCreator(reportSettings);
		}
		
		
		public override void Visit(ExportPage page)
		{
			UIElement = documentCreator.CreateFixedPage(page);
		}
	
		
		public override void Visit(ExportContainer exportColumn)
		{
			
//			Console.WriteLine("Wpf-Visit ExportContainer {0} - {1} - {2} - {3}", exportColumn.Name,exportColumn.Size,
//			                  exportColumn.Location,exportColumn.BackColor);
			
			var canvas = documentCreator.CreateContainer(exportColumn);
			UIElement = canvas;
		}
		
		
		public override void Visit(ExportText exportColumn)
		{
//			Console.WriteLine("Wpf-Visit ExportText {0} - {1} - {2}", exportColumn.Name,exportColumn.Size,exportColumn.DesiredSize);
			var textBlock = documentCreator.CreateTextBlock(exportColumn);
			UIElement = textBlock;
		}
		
		
		public override void Visit(ExportColumn exportColumn)
		{
//			Console.WriteLine("Wpf-Visit ExportColumn {0} - {1} - {2}", exportColumn.Name,exportColumn.Location,exportColumn.Size,);
		}
		
		public UIElement UIElement {get; private set;}
		
	}
}
