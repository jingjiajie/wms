using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WMS.TableGenerator
{
    class JsBasicFunctions
    {
        public string GetAllJsFuncStr()
        {
            StringBuilder sb = new StringBuilder();
            foreach(string strFunc in listFunc)
            {
                sb.Append(strFunc);
            }
            return sb.ToString();
        }

        private List<string> listFunc = new List<string>()
        {
            @"function range(/*start,end*/){
                            var start = 0;
                            var end = 0;
                            if(arguments.length == 1){
                                end = arguments[0];
                            }else{
                                start = arguments[0];
                                end = arguments[1];
                            }
                            var result = []
                            for(var i=start;i<end;i++){
                                result.push(i);
                            }
                            return result;
            }",
        };
    }
}
