using Cyberpunk2077HackHelper.Common;
using Cyberpunk2077HackHelper.Grabbing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Cyberpunk2077HackHelper.LayoutMarker
{
	public class XmlFileService : IFileService
	{
		[XmlType(TypeName = "Layout")]
		public class LayoutDto
		{
			public LayoutTableDto Matrix;
			public LayoutTableDto Sequences;
		}

		public class LayoutTableDto
		{
			public PointDto Position;
			public SizeDto CellSize;
			public SizeDto CellCount;

			public List<SymbolMapDto> SymbolMaps;
		}

		[XmlType(TypeName = "SymbolMap")]
		public class SymbolMapDto
		{
			[XmlAttribute]
			public Symbol Symbol;
			public List<PointDto> Points;
		}

		[XmlType(TypeName = "Point")]
		public class PointDto
		{
			[XmlAttribute]
			public int X;

			[XmlAttribute]
			public int Y;
		}

		[XmlType(TypeName = "Size")]
		public class SizeDto
		{
			[XmlAttribute]
			public int Width;

			[XmlAttribute]
			public int Height;
		}

		private readonly XmlSerializer _serializer = new XmlSerializer(typeof(LayoutDto));

		public Layout Open(string filename)
		{
			using (FileStream stream = File.OpenRead(filename))
			{
				LayoutDto deserializedLayout = (LayoutDto)_serializer.Deserialize(stream);
				stream.Close();

				return FromDto(deserializedLayout);
			}
		}

		public void Save(string filename, Layout layout)
		{
			LayoutDto dto = ToDto(layout);

			using (FileStream stream = File.Create(filename))
			{
				_serializer.Serialize(stream, dto);
				stream.Close();
			}
		}

		private LayoutDto ToDto(Layout layout)
		{
			return new LayoutDto()
			{
				Matrix = ToDto(layout.Matrix),
				Sequences = ToDto(layout.Sequences)
			};
		}

		private LayoutTableDto ToDto(LayoutTable layoutTable)
		{
			return new LayoutTableDto()
			{
				Position = ToDto(layoutTable.Position),
				CellCount = ToDto(layoutTable.CellCount),
				CellSize = ToDto(layoutTable.CellSize),
				SymbolMaps = layoutTable.SymbolMaps.Select(ToDto).ToList()
			};
		}

		private SymbolMapDto ToDto(SymbolMap symbolMap)
		{
			return new SymbolMapDto()
			{
				Symbol = symbolMap.Symbol,
				Points = symbolMap.Points.Select(ToDto).ToList()
			};
		}

		private PointDto ToDto(Point point)
		{
			return new PointDto() { X = point.X, Y = point.Y };
		}

		private SizeDto ToDto(Size size)
		{
			return new SizeDto() { Width = size.Width, Height = size.Height };
		}

		private Layout FromDto(LayoutDto layout)
		{
			return new Layout(FromDto(layout.Matrix), FromDto(layout.Sequences));
		}

		private LayoutTable FromDto(LayoutTableDto layoutTable)
		{
			return new LayoutTable()
			{
				Position = FromDto(layoutTable.Position),
				CellCount = FromDto(layoutTable.CellCount),
				CellSize = FromDto(layoutTable.CellSize),
				SymbolMaps = layoutTable.SymbolMaps.Select(FromDto).ToList()
			};
		}

		private SymbolMap FromDto(SymbolMapDto symbolMap)
		{
			return new SymbolMap(symbolMap.Symbol, symbolMap.Points.Select(FromDto).ToList());
		}

		private Point FromDto(PointDto point)
		{
			return new Point(point.X, point.Y);
		}

		private Size FromDto(SizeDto size)
		{
			return new Size(size.Width, size.Height);
		}
	}
}
