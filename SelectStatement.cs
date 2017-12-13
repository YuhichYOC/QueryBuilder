/*
*
* SelectStatement.cs
*
* Copyright 2017 Yuichi Yoshii
*     吉井雄一 @ 吉井産業  you.65535.kir@gmail.com
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*     http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*
*/
using System.Collections.Generic;

namespace QueryBuilder
{
    public class SelectStatement
    {
        private string table;
        public void SetTable(string arg)
        {
            table = arg;
        }

        private string tableAlias;
        public void SetTableAlias(string arg)
        {
            tableAlias = arg;
        }

        private List<Column> columns;
        private void SetColumns(List<Column> arg)
        {
            columns = arg;
        }

        public void AddColumn(string name, string tableAlias)
        {
            Column add = new Column();
            add.SetName(name);
            add.SetTableAlias(tableAlias);
            add.SetIndex(columns.Count);
            columns.Add(add);
        }

        private List<JoinKeyword> joins;
        private void SetJoins(List<JoinKeyword> arg)
        {
            joins = arg;
        }

        public void AddJoin(JoinKeyword arg)
        {
            joins.Add(arg);
        }

        private WhereKeyword where;
        public void SetWhere(WhereKeyword arg)
        {
            where = arg;
        }

        public SelectStatement()
        {
            columns = new List<Column>();
            joins = new List<JoinKeyword>();
        }

        public override string ToString()
        {
            string ret = string.Empty;

            ret += @" SELECT " + "\r\n";
            foreach (var item in columns)
            {
                ret += item.ToString() + "\r\n";
            }
            ret += @" FROM " + "\r\n";
            ret += @"        " + table + @" " + tableAlias + @" " + "\r\n";
            foreach (var item in joins)
            {
                ret += item.ToString();
            }
            if (where != null)
            {
                ret += where.ToString();
            }

            return ret;
        }

        class Column
        {
            private string name;
            public void SetName(string arg)
            {
                name = arg;
            }

            private string tableAlias;
            public void SetTableAlias(string arg)
            {
                tableAlias = arg;
            }

            private int index;
            public void SetIndex(int arg)
            {
                index = arg;
            }

            public override string ToString()
            {
                string ret = string.Empty;

                if (index == 0)
                {
                    ret += @"        ";
                }
                else
                {
                    ret += @"      , ";
                }
                ret += tableAlias + @"." + name + @" ";

                return ret;
            }
        }
    }
}
