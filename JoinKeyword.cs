/*
*
* JoinKeyword.cs
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
    public class JoinKeyword
    {
        private string joinTable;
        public void SetJoinTable(string arg)
        {
            joinTable = arg;
        }

        private string joinTableAlias;
        public void SetJoinTableAlias(string arg)
        {
            joinTableAlias = arg;
        }

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

        private bool innerJoin;
        public void SetInnerJoin(bool arg)
        {
            innerJoin = arg;
        }

        private bool leftOuterJoin;
        public void SetLeftOuterJoin(bool arg)
        {
            leftOuterJoin = arg;
        }

        private bool rightOuterJoin;
        public void SetRightOuterJoin(bool arg)
        {
            rightOuterJoin = arg;
        }

        private bool crossJoin;
        public void SetCrossJoin(bool arg)
        {
            crossJoin = arg;
        }

        private List<Condition> conditions;
        private void SetConditions(List<Condition> arg)
        {
            conditions = arg;
        }

        public void AddCondition(bool equal, bool greaterThanEqual, bool joinTableAsLarger
            , string joinTableColumn, string tableColumn)
        {
            if (conditions == null)
            {
                conditions = new List<Condition>();
            }
            Condition add = new Condition();
            add.SetJoinTableAlias(joinTableAlias);
            add.SetTableAlias(tableAlias);
            add.SetEqual(equal);
            add.SetGreaterThanEqual(greaterThanEqual);
            add.SetJoinTableAsLarger(joinTableAsLarger);
            add.SetJoinTableColumn(joinTableColumn);
            add.SetTableColumn(tableColumn);
            conditions.Add(add);
        }

        public override string ToString()
        {
            string ret = string.Empty;

            ret += JoinClause() + "\r\n";
            if (crossJoin)
            {
                return ret;
            }
            for (int i = 0; i < conditions.Count; i++)
            {
                if (i == 0)
                {
                    ret += @" ON " + "\r\n";
                    ret += @"        " + conditions[i].ToString() + @" " + "\r\n";
                }
                else
                {
                    ret += @"    AND " + "\r\n";
                    ret += @"        " + conditions[i].ToString() + @" " + "\r\n";
                }
            }

            return ret;
        }

        public string JoinClause()
        {
            if (innerJoin)
            {
                return @" INNER JOIN " + "\r\n" +
                       @"        " + joinTable + @" " + joinTableAlias + @" ";
            }
            else if (leftOuterJoin)
            {
                return @" LEFT OUTER JOIN " + "\r\n" +
                       @"        " + joinTable + @" " + joinTableAlias + @" ";
            }
            else if (rightOuterJoin)
            {
                return @" RIGHT OUTER JOIN " + "\r\n" +
                       @"        " + joinTable + @" " + joinTableAlias + @" ";
            }
            else if (crossJoin)
            {
                return @" CROSS JOIN " + "\r\n" +
                       @"        " + joinTable + @" " + joinTableAlias + @" ";
            }
            else
            {
                return @" JOIN " + "\r\n" +
                       @"        " + joinTable + @" " + joinTableAlias + @" ";
            }
        }

        class Condition
        {
            private string joinTableAlias;
            public void SetJoinTableAlias(string arg)
            {
                joinTableAlias = arg;
            }

            private string tableAlias;
            public void SetTableAlias(string arg)
            {
                tableAlias = arg;
            }

            private bool equal;
            public void SetEqual(bool arg)
            {
                equal = arg;
            }

            private bool greaterThanEqual;
            public void SetGreaterThanEqual(bool arg)
            {
                greaterThanEqual = arg;
            }

            private bool joinTableAsLarger;
            public void SetJoinTableAsLarger(bool arg)
            {
                joinTableAsLarger = arg;
            }

            private string joinTableColumn;
            public void SetJoinTableColumn(string arg)
            {
                joinTableColumn = arg;
            }

            private string tableColumn;
            public void SetTableColumn(string arg)
            {
                tableColumn = arg;
            }

            public Condition()
            {
                joinTableAlias = string.Empty;
                tableAlias = string.Empty;
                equal = true;
                greaterThanEqual = false;
                joinTableAsLarger = false;
                joinTableColumn = string.Empty;
                tableColumn = string.Empty;
            }

            public override string ToString()
            {
                return tableAlias + @"." + tableColumn 
                    + EqualClause() 
                    + joinTableAlias + @"." + joinTableColumn;
            }

            private string EqualClause()
            {
                if (equal)
                {
                    return @" = ";
                }
                else
                {
                    string ret = string.Empty;
                    if (joinTableAsLarger)
                    {
                        ret = @" <";
                    }
                    else
                    {
                        ret = @" >";
                    }
                    if (greaterThanEqual)
                    {
                        ret += @"= ";
                    }
                    else
                    {
                        ret += @" ";
                    }
                    return ret;
                }
            }
        }

    }
}
