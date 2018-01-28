using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr4.Runtime.Misc;

namespace EMacro
{
    class CompileListener : EMacroBaseListener
    {
        List<Command> commandList;
        public List<Command> CommandList { get => commandList; set => commandList = value; }
        

        public CompileListener(List<Command> commandList)
        {
            this.CommandList = commandList;
        }

        public override void EnterText([NotNull] EMacroParser.TextContext context)
        {
            var text = context.TEXT();
            commandList.Add(new Command.TEXT(text.GetText()));
        }

        public override void EnterWriteCommand([NotNull] EMacroParser.WriteCommandContext context)
        {
            string jsText = context.jsCommand().JSTEXT().GetText();
            commandList.Add(new Command.WRITE(jsText));
        }

        public override void EnterRepeatCommand([NotNull] EMacroParser.RepeatCommandContext context)
        {
            int rows = Convert.ToInt32(context.DIGIT()[0].GetText());
            int columns = Convert.ToInt32(context.DIGIT()[1].GetText());
            string varName = context.ID().GetText();
            string jsExprRange = context.jsCommand().JSTEXT().GetText();
            commandList.Add(new Command.REPEAT(rows, columns, varName, jsExprRange));
        }

        public override void EnterSetColorCommand([NotNull] EMacroParser.SetColorCommandContext context)
        {
            string jsText = context.jsCommand().JSTEXT().GetText();
            commandList.Add(new Command.SET_COLOR(jsText));
        }

        public override void EnterSetTableColumnsCommand([NotNull] EMacroParser.SetTableColumnsCommandContext context)
        {
            string jsText = context.jsCommand().JSTEXT().GetText();
            commandList.Add(new Command.SET_TABLE_COLUMNS(jsText));
        }
    }
}
