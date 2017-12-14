/*
*
* WhereKeyword.cs
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

namespace QueryBuilder {
    public class WhereKeyword {
        private List<Condition> conditions;
        private void SetConditions(List<Condition> arg) {
            conditions = arg;
        }

        public void AddCondition(bool equal, bool greaterThanEqual, bool rightSideAsLarger
            , string alias, string name, string value) {
            if (conditions == null) {
                conditions = new List<Condition>();
            }
            Condition add = new Condition();
            add.SetEqual(equal);
            add.SetGreaterThanEqual(greaterThanEqual);
            add.SetRightSideAsLarger(rightSideAsLarger);
            add.SetLeftSide(alias, name);
            add.SetRightSide(value);
            conditions.Add(add);
        }

        public override string ToString() {
            string ret = string.Empty;

            ret += @" WHERE " + "\r\n";
            for (int i = 0; i < conditions.Count; i++) {
                if (i == 0) {
                    ret += @"        " + conditions[i].ToString() + @" " + "\r\n";
                }
                else {
                    ret += @"    AND " + "\r\n";
                    ret += @"        " + conditions[i].ToString() + @" " + "\r\n";
                }
            }

            return ret;
        }

        class Condition {
            private bool equal;
            public void SetEqual(bool arg) {
                equal = arg;
            }

            private bool greaterThanEqual;
            public void SetGreaterThanEqual(bool arg) {
                greaterThanEqual = arg;
            }

            private bool rightSideAsLarger;
            public void SetRightSideAsLarger(bool arg) {
                rightSideAsLarger = arg;
            }

            private Side leftSide;
            private void SetLeftSide(Side arg) {
                leftSide = arg;
            }

            public void SetLeftSide(string alias, string name) {
                leftSide = new Side();
                leftSide.SetAlias(alias);
                leftSide.SetName(name);
            }

            private string rightSide;
            public void SetRightSide(string arg) {
                rightSide = arg;
            }

            public Condition() {
                equal = true;
                greaterThanEqual = false;
                rightSideAsLarger = false;
            }

            public override string ToString() {
                return leftSide.ToString()
                    + EqualClause()
                    + rightSide;
            }

            private string EqualClause() {
                if (equal) {
                    return @" = ";
                }
                else {
                    string ret = string.Empty;
                    if (rightSideAsLarger) {
                        ret += @" <";
                    }
                    else {
                        ret += @" >";
                    }
                    if (greaterThanEqual) {
                        ret += @"= ";
                    }
                    else {
                        ret += @" ";
                    }
                    return ret;
                }
            }

            class Side {
                private string alias;
                public void SetAlias(string arg) {
                    alias = arg;
                }

                private string name;
                public void SetName(string arg) {
                    name = arg;
                }

                public override string ToString() {
                    return alias + @"." + name;
                }
            }
        }
    }
}
