﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#nullable enable

using NUnit.Framework;

namespace CodeJam
{
	[TestFixture]
	public class OneOfTests
	{
		private class Case1
		{
			public string Name => "Case1";
		}

		private class Case2
		{
			public string Name => "Case2";
		}

		private class Case3
		{
			public string Name => "Case3";
		}

		private class Case4
		{
			public string Name => "Case4";
		}

		private class Case5
		{
			public string Name => "Case5";
		}

		private class Case6
		{
			public string Name => "Case6";
		}

		private class Case7
		{
			public string Name => "Case7";
		}

		private class Case8
		{
			public string Name => "Case8";
		}


		[Test]
		public void OneOf2Test()
		{
			string str;
			var item1 = OneOf<Case1, Case2>.Create(new Case1());
			Assert.AreEqual("Case1", item1.GetValue(v => v.Name, v => v.Name));

			str = "";
			item1.Do(v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case1", str);

			var item2 = OneOf<Case1, Case2>.Create(new Case2());
			Assert.AreEqual("Case2", item2.GetValue(v => v.Name, v => v.Name));

			str = "";
			item2.Do(v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case2", str);

		}

		[Test]
		public void ValueOneOf2Test()
		{
			string str;
			var item1 = ValueOneOf<Case1, Case2>.Create(new Case1());
			Assert.AreEqual("Case1", item1.GetValue(v => v.Name, v => v.Name));

			str = "";
			item1.Do(v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case1", str);

			var item2 = ValueOneOf<Case1, Case2>.Create(new Case2());
			Assert.AreEqual("Case2", item2.GetValue(v => v.Name, v => v.Name));

			str = "";
			item2.Do(v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case2", str);

		}

		[Test]
		public void OneOf3Test()
		{
			string str;
			var item1 = OneOf<Case1, Case2, Case3>.Create(new Case1());
			Assert.AreEqual("Case1", item1.GetValue(v => v.Name, v => v.Name, v => v.Name));

			str = "";
			item1.Do(v => str = v.Name, v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case1", str);

			var item2 = OneOf<Case1, Case2, Case3>.Create(new Case2());
			Assert.AreEqual("Case2", item2.GetValue(v => v.Name, v => v.Name, v => v.Name));

			str = "";
			item2.Do(v => str = v.Name, v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case2", str);

			var item3 = OneOf<Case1, Case2, Case3>.Create(new Case3());
			Assert.AreEqual("Case3", item3.GetValue(v => v.Name, v => v.Name, v => v.Name));

			str = "";
			item3.Do(v => str = v.Name, v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case3", str);

		}

		[Test]
		public void ValueOneOf3Test()
		{
			string str;
			var item1 = ValueOneOf<Case1, Case2, Case3>.Create(new Case1());
			Assert.AreEqual("Case1", item1.GetValue(v => v.Name, v => v.Name, v => v.Name));

			str = "";
			item1.Do(v => str = v.Name, v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case1", str);

			var item2 = ValueOneOf<Case1, Case2, Case3>.Create(new Case2());
			Assert.AreEqual("Case2", item2.GetValue(v => v.Name, v => v.Name, v => v.Name));

			str = "";
			item2.Do(v => str = v.Name, v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case2", str);

			var item3 = ValueOneOf<Case1, Case2, Case3>.Create(new Case3());
			Assert.AreEqual("Case3", item3.GetValue(v => v.Name, v => v.Name, v => v.Name));

			str = "";
			item3.Do(v => str = v.Name, v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case3", str);

		}

		[Test]
		public void OneOf4Test()
		{
			string str;
			var item1 = OneOf<Case1, Case2, Case3, Case4>.Create(new Case1());
			Assert.AreEqual("Case1", item1.GetValue(v => v.Name, v => v.Name, v => v.Name, v => v.Name));

			str = "";
			item1.Do(v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case1", str);

			var item2 = OneOf<Case1, Case2, Case3, Case4>.Create(new Case2());
			Assert.AreEqual("Case2", item2.GetValue(v => v.Name, v => v.Name, v => v.Name, v => v.Name));

			str = "";
			item2.Do(v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case2", str);

			var item3 = OneOf<Case1, Case2, Case3, Case4>.Create(new Case3());
			Assert.AreEqual("Case3", item3.GetValue(v => v.Name, v => v.Name, v => v.Name, v => v.Name));

			str = "";
			item3.Do(v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case3", str);

			var item4 = OneOf<Case1, Case2, Case3, Case4>.Create(new Case4());
			Assert.AreEqual("Case4", item4.GetValue(v => v.Name, v => v.Name, v => v.Name, v => v.Name));

			str = "";
			item4.Do(v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case4", str);

		}

		[Test]
		public void ValueOneOf4Test()
		{
			string str;
			var item1 = ValueOneOf<Case1, Case2, Case3, Case4>.Create(new Case1());
			Assert.AreEqual("Case1", item1.GetValue(v => v.Name, v => v.Name, v => v.Name, v => v.Name));

			str = "";
			item1.Do(v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case1", str);

			var item2 = ValueOneOf<Case1, Case2, Case3, Case4>.Create(new Case2());
			Assert.AreEqual("Case2", item2.GetValue(v => v.Name, v => v.Name, v => v.Name, v => v.Name));

			str = "";
			item2.Do(v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case2", str);

			var item3 = ValueOneOf<Case1, Case2, Case3, Case4>.Create(new Case3());
			Assert.AreEqual("Case3", item3.GetValue(v => v.Name, v => v.Name, v => v.Name, v => v.Name));

			str = "";
			item3.Do(v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case3", str);

			var item4 = ValueOneOf<Case1, Case2, Case3, Case4>.Create(new Case4());
			Assert.AreEqual("Case4", item4.GetValue(v => v.Name, v => v.Name, v => v.Name, v => v.Name));

			str = "";
			item4.Do(v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case4", str);

		}

		[Test]
		public void OneOf5Test()
		{
			string str;
			var item1 = OneOf<Case1, Case2, Case3, Case4, Case5>.Create(new Case1());
			Assert.AreEqual("Case1", item1.GetValue(v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name));

			str = "";
			item1.Do(v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case1", str);

			var item2 = OneOf<Case1, Case2, Case3, Case4, Case5>.Create(new Case2());
			Assert.AreEqual("Case2", item2.GetValue(v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name));

			str = "";
			item2.Do(v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case2", str);

			var item3 = OneOf<Case1, Case2, Case3, Case4, Case5>.Create(new Case3());
			Assert.AreEqual("Case3", item3.GetValue(v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name));

			str = "";
			item3.Do(v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case3", str);

			var item4 = OneOf<Case1, Case2, Case3, Case4, Case5>.Create(new Case4());
			Assert.AreEqual("Case4", item4.GetValue(v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name));

			str = "";
			item4.Do(v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case4", str);

			var item5 = OneOf<Case1, Case2, Case3, Case4, Case5>.Create(new Case5());
			Assert.AreEqual("Case5", item5.GetValue(v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name));

			str = "";
			item5.Do(v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case5", str);

		}

		[Test]
		public void ValueOneOf5Test()
		{
			string str;
			var item1 = ValueOneOf<Case1, Case2, Case3, Case4, Case5>.Create(new Case1());
			Assert.AreEqual("Case1", item1.GetValue(v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name));

			str = "";
			item1.Do(v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case1", str);

			var item2 = ValueOneOf<Case1, Case2, Case3, Case4, Case5>.Create(new Case2());
			Assert.AreEqual("Case2", item2.GetValue(v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name));

			str = "";
			item2.Do(v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case2", str);

			var item3 = ValueOneOf<Case1, Case2, Case3, Case4, Case5>.Create(new Case3());
			Assert.AreEqual("Case3", item3.GetValue(v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name));

			str = "";
			item3.Do(v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case3", str);

			var item4 = ValueOneOf<Case1, Case2, Case3, Case4, Case5>.Create(new Case4());
			Assert.AreEqual("Case4", item4.GetValue(v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name));

			str = "";
			item4.Do(v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case4", str);

			var item5 = ValueOneOf<Case1, Case2, Case3, Case4, Case5>.Create(new Case5());
			Assert.AreEqual("Case5", item5.GetValue(v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name));

			str = "";
			item5.Do(v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case5", str);

		}

		[Test]
		public void OneOf6Test()
		{
			string str;
			var item1 = OneOf<Case1, Case2, Case3, Case4, Case5, Case6>.Create(new Case1());
			Assert.AreEqual("Case1", item1.GetValue(v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name));

			str = "";
			item1.Do(v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case1", str);

			var item2 = OneOf<Case1, Case2, Case3, Case4, Case5, Case6>.Create(new Case2());
			Assert.AreEqual("Case2", item2.GetValue(v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name));

			str = "";
			item2.Do(v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case2", str);

			var item3 = OneOf<Case1, Case2, Case3, Case4, Case5, Case6>.Create(new Case3());
			Assert.AreEqual("Case3", item3.GetValue(v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name));

			str = "";
			item3.Do(v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case3", str);

			var item4 = OneOf<Case1, Case2, Case3, Case4, Case5, Case6>.Create(new Case4());
			Assert.AreEqual("Case4", item4.GetValue(v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name));

			str = "";
			item4.Do(v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case4", str);

			var item5 = OneOf<Case1, Case2, Case3, Case4, Case5, Case6>.Create(new Case5());
			Assert.AreEqual("Case5", item5.GetValue(v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name));

			str = "";
			item5.Do(v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case5", str);

			var item6 = OneOf<Case1, Case2, Case3, Case4, Case5, Case6>.Create(new Case6());
			Assert.AreEqual("Case6", item6.GetValue(v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name));

			str = "";
			item6.Do(v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case6", str);

		}

		[Test]
		public void ValueOneOf6Test()
		{
			string str;
			var item1 = ValueOneOf<Case1, Case2, Case3, Case4, Case5, Case6>.Create(new Case1());
			Assert.AreEqual("Case1", item1.GetValue(v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name));

			str = "";
			item1.Do(v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case1", str);

			var item2 = ValueOneOf<Case1, Case2, Case3, Case4, Case5, Case6>.Create(new Case2());
			Assert.AreEqual("Case2", item2.GetValue(v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name));

			str = "";
			item2.Do(v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case2", str);

			var item3 = ValueOneOf<Case1, Case2, Case3, Case4, Case5, Case6>.Create(new Case3());
			Assert.AreEqual("Case3", item3.GetValue(v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name));

			str = "";
			item3.Do(v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case3", str);

			var item4 = ValueOneOf<Case1, Case2, Case3, Case4, Case5, Case6>.Create(new Case4());
			Assert.AreEqual("Case4", item4.GetValue(v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name));

			str = "";
			item4.Do(v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case4", str);

			var item5 = ValueOneOf<Case1, Case2, Case3, Case4, Case5, Case6>.Create(new Case5());
			Assert.AreEqual("Case5", item5.GetValue(v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name));

			str = "";
			item5.Do(v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case5", str);

			var item6 = ValueOneOf<Case1, Case2, Case3, Case4, Case5, Case6>.Create(new Case6());
			Assert.AreEqual("Case6", item6.GetValue(v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name));

			str = "";
			item6.Do(v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case6", str);

		}

		[Test]
		public void OneOf7Test()
		{
			string str;
			var item1 = OneOf<Case1, Case2, Case3, Case4, Case5, Case6, Case7>.Create(new Case1());
			Assert.AreEqual("Case1", item1.GetValue(v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name));

			str = "";
			item1.Do(v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case1", str);

			var item2 = OneOf<Case1, Case2, Case3, Case4, Case5, Case6, Case7>.Create(new Case2());
			Assert.AreEqual("Case2", item2.GetValue(v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name));

			str = "";
			item2.Do(v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case2", str);

			var item3 = OneOf<Case1, Case2, Case3, Case4, Case5, Case6, Case7>.Create(new Case3());
			Assert.AreEqual("Case3", item3.GetValue(v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name));

			str = "";
			item3.Do(v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case3", str);

			var item4 = OneOf<Case1, Case2, Case3, Case4, Case5, Case6, Case7>.Create(new Case4());
			Assert.AreEqual("Case4", item4.GetValue(v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name));

			str = "";
			item4.Do(v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case4", str);

			var item5 = OneOf<Case1, Case2, Case3, Case4, Case5, Case6, Case7>.Create(new Case5());
			Assert.AreEqual("Case5", item5.GetValue(v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name));

			str = "";
			item5.Do(v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case5", str);

			var item6 = OneOf<Case1, Case2, Case3, Case4, Case5, Case6, Case7>.Create(new Case6());
			Assert.AreEqual("Case6", item6.GetValue(v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name));

			str = "";
			item6.Do(v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case6", str);

			var item7 = OneOf<Case1, Case2, Case3, Case4, Case5, Case6, Case7>.Create(new Case7());
			Assert.AreEqual("Case7", item7.GetValue(v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name));

			str = "";
			item7.Do(v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case7", str);

		}

		[Test]
		public void ValueOneOf7Test()
		{
			string str;
			var item1 = ValueOneOf<Case1, Case2, Case3, Case4, Case5, Case6, Case7>.Create(new Case1());
			Assert.AreEqual("Case1", item1.GetValue(v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name));

			str = "";
			item1.Do(v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case1", str);

			var item2 = ValueOneOf<Case1, Case2, Case3, Case4, Case5, Case6, Case7>.Create(new Case2());
			Assert.AreEqual("Case2", item2.GetValue(v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name));

			str = "";
			item2.Do(v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case2", str);

			var item3 = ValueOneOf<Case1, Case2, Case3, Case4, Case5, Case6, Case7>.Create(new Case3());
			Assert.AreEqual("Case3", item3.GetValue(v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name));

			str = "";
			item3.Do(v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case3", str);

			var item4 = ValueOneOf<Case1, Case2, Case3, Case4, Case5, Case6, Case7>.Create(new Case4());
			Assert.AreEqual("Case4", item4.GetValue(v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name));

			str = "";
			item4.Do(v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case4", str);

			var item5 = ValueOneOf<Case1, Case2, Case3, Case4, Case5, Case6, Case7>.Create(new Case5());
			Assert.AreEqual("Case5", item5.GetValue(v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name));

			str = "";
			item5.Do(v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case5", str);

			var item6 = ValueOneOf<Case1, Case2, Case3, Case4, Case5, Case6, Case7>.Create(new Case6());
			Assert.AreEqual("Case6", item6.GetValue(v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name));

			str = "";
			item6.Do(v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case6", str);

			var item7 = ValueOneOf<Case1, Case2, Case3, Case4, Case5, Case6, Case7>.Create(new Case7());
			Assert.AreEqual("Case7", item7.GetValue(v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name));

			str = "";
			item7.Do(v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case7", str);

		}

		[Test]
		public void OneOf8Test()
		{
			string str;
			var item1 = OneOf<Case1, Case2, Case3, Case4, Case5, Case6, Case7, Case8>.Create(new Case1());
			Assert.AreEqual("Case1", item1.GetValue(v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name));

			str = "";
			item1.Do(v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case1", str);

			var item2 = OneOf<Case1, Case2, Case3, Case4, Case5, Case6, Case7, Case8>.Create(new Case2());
			Assert.AreEqual("Case2", item2.GetValue(v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name));

			str = "";
			item2.Do(v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case2", str);

			var item3 = OneOf<Case1, Case2, Case3, Case4, Case5, Case6, Case7, Case8>.Create(new Case3());
			Assert.AreEqual("Case3", item3.GetValue(v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name));

			str = "";
			item3.Do(v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case3", str);

			var item4 = OneOf<Case1, Case2, Case3, Case4, Case5, Case6, Case7, Case8>.Create(new Case4());
			Assert.AreEqual("Case4", item4.GetValue(v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name));

			str = "";
			item4.Do(v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case4", str);

			var item5 = OneOf<Case1, Case2, Case3, Case4, Case5, Case6, Case7, Case8>.Create(new Case5());
			Assert.AreEqual("Case5", item5.GetValue(v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name));

			str = "";
			item5.Do(v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case5", str);

			var item6 = OneOf<Case1, Case2, Case3, Case4, Case5, Case6, Case7, Case8>.Create(new Case6());
			Assert.AreEqual("Case6", item6.GetValue(v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name));

			str = "";
			item6.Do(v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case6", str);

			var item7 = OneOf<Case1, Case2, Case3, Case4, Case5, Case6, Case7, Case8>.Create(new Case7());
			Assert.AreEqual("Case7", item7.GetValue(v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name));

			str = "";
			item7.Do(v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case7", str);

			var item8 = OneOf<Case1, Case2, Case3, Case4, Case5, Case6, Case7, Case8>.Create(new Case8());
			Assert.AreEqual("Case8", item8.GetValue(v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name));

			str = "";
			item8.Do(v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case8", str);

		}

		[Test]
		public void ValueOneOf8Test()
		{
			string str;
			var item1 = ValueOneOf<Case1, Case2, Case3, Case4, Case5, Case6, Case7, Case8>.Create(new Case1());
			Assert.AreEqual("Case1", item1.GetValue(v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name));

			str = "";
			item1.Do(v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case1", str);

			var item2 = ValueOneOf<Case1, Case2, Case3, Case4, Case5, Case6, Case7, Case8>.Create(new Case2());
			Assert.AreEqual("Case2", item2.GetValue(v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name));

			str = "";
			item2.Do(v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case2", str);

			var item3 = ValueOneOf<Case1, Case2, Case3, Case4, Case5, Case6, Case7, Case8>.Create(new Case3());
			Assert.AreEqual("Case3", item3.GetValue(v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name));

			str = "";
			item3.Do(v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case3", str);

			var item4 = ValueOneOf<Case1, Case2, Case3, Case4, Case5, Case6, Case7, Case8>.Create(new Case4());
			Assert.AreEqual("Case4", item4.GetValue(v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name));

			str = "";
			item4.Do(v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case4", str);

			var item5 = ValueOneOf<Case1, Case2, Case3, Case4, Case5, Case6, Case7, Case8>.Create(new Case5());
			Assert.AreEqual("Case5", item5.GetValue(v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name));

			str = "";
			item5.Do(v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case5", str);

			var item6 = ValueOneOf<Case1, Case2, Case3, Case4, Case5, Case6, Case7, Case8>.Create(new Case6());
			Assert.AreEqual("Case6", item6.GetValue(v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name));

			str = "";
			item6.Do(v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case6", str);

			var item7 = ValueOneOf<Case1, Case2, Case3, Case4, Case5, Case6, Case7, Case8>.Create(new Case7());
			Assert.AreEqual("Case7", item7.GetValue(v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name));

			str = "";
			item7.Do(v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case7", str);

			var item8 = ValueOneOf<Case1, Case2, Case3, Case4, Case5, Case6, Case7, Case8>.Create(new Case8());
			Assert.AreEqual("Case8", item8.GetValue(v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name, v => v.Name));

			str = "";
			item8.Do(v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name, v => str = v.Name);
			Assert.AreEqual("Case8", str);

		}

	}
}