/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
lexer grammar EMacroLexer;

TEXT_SHARP : '#' -> mode(MODE_COMMAND);
TEXT_AT : '@' -> mode(MODE_JSLEX);
TEXT : ~[#@]+;

mode MODE_COMMAND;
WS : [ \t\r\n]+ -> skip ;
COMMAND_SHARP : '#';
REPEAT : 'REPEAT';
AREA : 'AREA' ;
VAR : 'VAR' ;
IN : 'IN' -> mode(MODE_JSLEX);

WRITE : 'WRITE'-> mode(MODE_JSLEX);

SET : 'SET';
COLOR : 'COLOR' -> mode(MODE_JSLEX);

DIGIT : [0-9]+;
ID : [a-zA-Z][a-zA-Z0-9]* ;

mode MODE_JSLEX;
JS_SHARP : '#' -> mode(MODE_COMMAND);
JS_AT : '@' -> mode(MODE_JSLEX);
JSTEXT : ~[#@]+ ;