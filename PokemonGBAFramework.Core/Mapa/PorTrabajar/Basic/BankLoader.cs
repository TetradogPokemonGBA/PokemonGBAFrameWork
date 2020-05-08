using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokemonGBAFramework.Core.Mapa.Basic
{
	public class BankLoader
	{
		int tblOffs;
		//JLabel lbl;
		//JTree tree;
		//DefaultMutableTreeNode node;
		private static int mapNamesPtr;
		public static List<long>[] maps;
		public static List<long> bankPointers = new List<long>();
		public static bool banksLoaded = false;
		public static SortedList<int, String> mapNames = new SortedList<int, String>();

		public static void reset(RomGba rom,int mapLabels, int numBanks)
		{
			try
			{
				mapNamesPtr = new OffsetRom(rom,mapLabels);
				maps = new List<long>[numBanks];
				bankPointers = new List<long>();
				banksLoaded = false;
			}
			catch (Exception e)
			{

			}
		}

		//public BankLoader(int tableOffset, RomGba rom, JLabel label, JTree tree, DefaultMutableTreeNode node)
		//{

		//	tblOffs = (int)new OffsetRom(rom, tableOffset);

		//	lbl = label;
		//	this.tree = tree;
		//	this.node = node;
		//	reset(rom);
		//}


		public void run(RomGba rom,int numBanks,int[] mapBankSize,int mapsLabels,int engineVersion=1)
		{
			int mapNum = 0;
			List<byte[]> preMapList;
			int bankNum = 0;
			//Date d = new Date();
			int tblOffs = this.tblOffs;
			List<byte[]> bankPointersPre = new List<byte[]>();
			for (int i = 0; i < 4; i++)
			{
			bankPointersPre.Add(rom.Data.SubArray(tblOffs, numBanks));
				tblOffs += OffsetRom.LENGTH;
			}
			//DefaultTreeModel model = (DefaultTreeModel)tree.getModel();
			//DefaultMutableTreeNode root = node;


			foreach (byte[] b in bankPointersPre)
			{
				//setStatus("Loading banks into tree...\t" + bankNum);
				bankPointers.Add(Serializar.ToInt(b));
				//DefaultMutableTreeNode node = new DefaultMutableTreeNode(String.valueOf(bankNum));
				//model.insertNodeInto(node, root, root.getChildCount());
				bankNum++;
			}

	
			foreach (long l in bankPointers)
			{
				preMapList = new List<byte[]>();
				for (int i = 0; i < 4; i++)
				{
					bankPointersPre.Add(rom.Data.SubArray((int)l, mapBankSize[mapNum]));
				}

				List<long> mapList = new List<long>();
				int miniMapNum = 0;
				foreach (byte[] b in preMapList)
				{
					//setStatus("Loading maps into tree...\tBank " + mapNum + ", map " + miniMapNum);
					try
					{
						long dataPtr = Serializar.ToInt(b);
						mapList.Add(dataPtr);
						int mapName = rom.Data.Bytes[ (int)((dataPtr - (8 << 24)) + 0x14)];
						//mapName -= 0x58; //TODO: Add Jambo51's map header hack
						int mapNamePokePtr = 0;
						String convMapName = "";
						if (engineVersion == 1)
						{
							if (!mapNames.ContainsKey(mapName))
							{
								mapNamePokePtr =new OffsetRom(rom,(int)mapsLabels + ((mapName - 0x58) * 4)); //TODO use the actual structure
								convMapName = BloqueString.Get(rom, mapNamePokePtr);
								mapNames.Add(mapName, convMapName);
							}
							else
							{
								convMapName = mapNames[mapName];
							}
						}
						else if (engineVersion == 0)//RSE
						{
							if (!mapNames.ContainsKey(mapName))
							{
								mapNamePokePtr =new OffsetRom( rom,(int)mapsLabels + ((mapName * 8) + 4));
								convMapName = BloqueString.Get(rom, mapNamePokePtr);
								mapNames.Add(mapName, convMapName);
							}
							else
							{
								convMapName = mapNames[mapName];
							}
						}

						MapTreeNode node = new MapTreeNode(convMapName + " (" + mapNum + "." + miniMapNum + ")", mapNum, miniMapNum); //TODO: Pull PokeText from header
						//findNode(root, mapNum + "").add(node);
						miniMapNum++;
					}
					catch (Exception e)
					{
						Console.WriteLine(e.Message);
					}
				}
				maps[mapNum] = mapList;
				mapNum++;
			}

			//setStatus("Refreshing tree...");
			//model.reload(root);
			//for (int i = 0; i < tree.getRowCount(); i++)
			//{
			//	//TreePath path = tree.getPathForRow(i);
			//	//if (path != null)
			//	//{
			//	//	//javax.swing.tree.TreeNode node = (javax.swing.tree.TreeNode)path.getLastPathComponent();
			//	//	//String str = node.toString();
			//	//	//DefaultTreeModel models = (DefaultTreeModel)tree.getModel();
			//	//	//models.valueForPathChanged(path, str);
			//	//}
			//}
			//banksLoaded = true;

			//Date eD = new Date();

			//double loadTime = eD.getTime() - d.getTime();

			//setStatus("Banks loaded in " + loadTime + "ms" + (loadTime < 1000 ? "! :DDD" : "."));
		}

		//public void setStatus(String status)
		//{
		//	lbl.setText(status);
		//}

		//private TreePath findPath(DefaultMutableTreeNode root, String s)
		//{

		//	Enumeration<DefaultMutableTreeNode> e = root.depthFirstEnumeration();
		//	while (e.hasMoreElements())
		//	{
		//		DefaultMutableTreeNode node = e.nextElement();
		//		if (node.toString().equalsIgnoreCase(s))
		//		{
		//			return new TreePath(node.getPath());
		//		}
		//	}
		//	return null;
		//}

		//private DefaultMutableTreeNode findNode(DefaultMutableTreeNode root, String s)
		//{
		//	Enumeration<DefaultMutableTreeNode> e = root.depthFirstEnumeration();
		//	while (e.hasMoreElements())
		//	{
		//		DefaultMutableTreeNode node = e.nextElement();
		//		if (node.toString().equalsIgnoreCase(s))
		//		{
		//			return node;
		//		}
		//	}
		//	return null;
		//}

		public class MapTreeNode
		{
			public int bank;
			public int map;
			public string name;
			public MapTreeNode(String name, int bank2, int map2)
			{
				this.name = name;
				bank = bank2;
				map = map2;
			}
		}
	}

}
