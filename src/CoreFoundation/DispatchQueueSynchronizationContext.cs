//
// DispatchQueueSynchronizationContext.cs: Context for Grand Central Dispatch framework
//
// Authors:
//   Marek Safar (marek.safar@gmail.com)
//
// Copyright 2013 Xamarin Inc
//
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

#nullable enable

using System;
using System.Threading;

namespace CoreFoundation {

	sealed class DispatchQueueSynchronizationContext : SynchronizationContext
	{
#if !COREBUILD
		readonly DispatchQueue queue;

		public DispatchQueueSynchronizationContext (DispatchQueue dispatchQueue)
		{
			if (dispatchQueue is null)
				ObjCRuntime.ThrowHelper.ThrowArgumentNullException (nameof (dispatchQueue));
			this.queue = dispatchQueue;
		}

		public override SynchronizationContext CreateCopy ()
		{
			return new DispatchQueueSynchronizationContext (queue);
		}

		public override void Post (SendOrPostCallback d, object? state)
		{
			queue.DispatchAsync (() => d (state));
		}

		public override void Send (SendOrPostCallback d, object? state)
		{
			queue.DispatchSync (() => d (state));
		}
#endif // !COREBUILD
	}
}
