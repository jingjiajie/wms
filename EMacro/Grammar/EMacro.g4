/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
grammar EMacro;

options {tokenVocab=EMacroLexer;}

stat : text? command*;

command : writeCommand | repeatCommand | setColorCommand | setTableColumnsCommand;

text : TEXT;
commandStartSharp : TEXT_SHARP|JS_SHARP|COMMAND_SHARP;
commandStartAt : TEXT_AT | JS_AT;
writeCommand :  commandStartSharp WRITE jsCommand | commandStartAt jsCommand;
repeatCommand : commandStartSharp REPEAT AREA DIGIT DIGIT VAR ID IN jsCommand;
setColorCommand : commandStartSharp SET COLOR jsCommand;
setTableColumnsCommand: commandStartSharp SET TABLE_COLUMNS jsCommand;

jsCommand : JSTEXT;