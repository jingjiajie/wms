using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr4.Runtime.Misc;

namespace EGCMD
{
    class EGCMDCompileListener : EGCMDBaseListener
    {
        List<EGCMDCommand> commandList;
        public List<EGCMDCommand> CommandList { get => commandList; set => commandList = value; }

        public EGCMDCompileListener(List<EGCMDCommand> commandList)
        {
            this.CommandList = commandList;
        }

        public override void EnterText([NotNull] EGCMDParser.TextContext context)
        {
            var textChars = context.TEXT();
            if (textChars.Count() == 0)
            {
                return;
            }
            StringBuilder sb = new StringBuilder();
            foreach (var textChar in textChars)
            {
                sb.Append(textChar.GetText());
            }
            commandList.Add(new EGCMDCommand.TEXT(sb.ToString()));
        }

        public override void EnterWriteCommand([NotNull] EGCMDParser.WriteCommandContext context)
        {
            StringBuilder sb = new StringBuilder();
            var jsChars = context.jsCommand().JSCHAR();
            foreach(var jsChar in jsChars)
            {
                sb.Append(jsChar.GetText());
            }
            commandList.Add(new EGCMDCommand.WRITE(sb.ToString()));
        }

        public override void EnterRepeatCommand([NotNull] EGCMDParser.RepeatCommandContext context)
        {
            StringBuilder sb = new StringBuilder();
            var jsChars = context.jsCommand().JSCHAR();
            foreach (var jsChar in jsChars)
            {
                sb.Append(jsChar.GetText());
            }
            int rows = Convert.ToInt32(context.DIGIT()[0].GetText());
            int columns = Convert.ToInt32(context.DIGIT()[1].GetText());
            string varName = context.ID().GetText();
            string jsExprRange = sb.ToString();
            commandList.Add(new EGCMDCommand.REPEAT(rows, columns, varName, jsExprRange));
        }

        public override void EnterSetColorCommand([NotNull] EGCMDParser.SetColorCommandContext context)
        {
            StringBuilder sb = new StringBuilder();
            var jsChars = context.jsCommand().JSCHAR();
            foreach (var jsChar in jsChars)
            {
                sb.Append(jsChar.GetText());
            }
            commandList.Add(new EGCMDCommand.SET_COLOR(sb.ToString()));
        }
    }
}
