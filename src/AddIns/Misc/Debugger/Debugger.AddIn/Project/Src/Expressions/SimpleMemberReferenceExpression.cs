// <file>
//     <copyright license="BSD-new" see="prj:///COPYING"/>
//     <owner name="David Srbeck�" email="dsrbecky@gmail.com"/>
//     <version>$Revision$</version>
// </file>

using System;

using Debugger;

namespace Debugger.Expressions
{
	public class SimpleMemberReferenceExpression: Expression
	{
		Expression targetObject;
		string member;
		
		public Expression TargetObject {
			get { return targetObject; }
		}
		
		public string Member {
			get { return member; }
		}
		
		public SimpleMemberReferenceExpression(Expression targetObject, string member)
		{
			this.targetObject = targetObject;
			this.member = member;
		}
		
		public override string Code {
			get {
				return targetObject.Code + "." + member;
			}
		}
		
		protected override Value EvaluateInternal(StackFrame context)
		{
			Value targetValue = targetObject.Evaluate(context);
			return targetValue.GetMemberValue(member);
		}
	}
}
