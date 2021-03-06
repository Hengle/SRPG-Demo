﻿//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//
using System;
using Gamelogic.Extensions.Internal;

namespace Gamelogic.Grids
{
	/// <summary>
	/// The base class for all shape operators.A shape operator provides a mechanism
	/// to construct grids in a certain shape.Shape operators are used with the shape
	/// info classes.
	/// </summary>
	/// <typeparam name="TShapeInfo">ShapeInfo Class to be used.</typeparam>
	[Version(1)]
	public abstract class AbstractOp<TShapeInfo>
		where TShapeInfo : class
	{
		#region Fields

		public readonly TShapeInfo leftShapeInfo;
		public readonly Func<TShapeInfo, TShapeInfo, TShapeInfo>
			combineShapeInfo;

		#endregion

		#region Constructors

		protected AbstractOp()
		{
			leftShapeInfo = null;
			combineShapeInfo = (x, y) => y;
		}

		protected AbstractOp(
			TShapeInfo leftShapeInfo,
			Func<TShapeInfo, TShapeInfo, TShapeInfo> combineShapeInfo)
		{
			this.leftShapeInfo = leftShapeInfo;
			this.combineShapeInfo = combineShapeInfo;
		}

		#endregion

		#region Shape Info Makers

		public TShapeInfo MakeShapeStorageInfo(TShapeInfo shapeInfo)
		{
			return combineShapeInfo(leftShapeInfo, shapeInfo);
		}

		public TShapeInfo MakeShapeStorageInfo<TPoint>(int width, int height, Func<TPoint, bool> isInside)
		{
			var shapeInfo = (TShapeInfo)(object)new ShapeStorageInfo<TPoint>(width, height, isInside);

			return MakeShapeStorageInfo(shapeInfo);
		}

		public TShapeInfo MakeShapeStorageInfo<TPoint>(IntRect storageRect, Func<TPoint, bool> isInside)
		{
			var shapeInfo = (TShapeInfo)(object)new ShapeStorageInfo<TPoint>(storageRect, isInside);

			return MakeShapeStorageInfo(shapeInfo);
		}

		#endregion
	}
}