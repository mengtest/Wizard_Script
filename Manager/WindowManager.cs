﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI {
    public class WindowManager : Singleton<WindowManager> {

        public UIRoot uiRoot {
            get; set;
        }

        List<WindowViewBase> activedWin = new List<WindowViewBase>();
        List<WindowViewBase> unActivedWin = new List<WindowViewBase>();

        public override void Init () {

        }

        /// <summary>
        /// 打开窗口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Open<T> () where T : WindowViewBase, new() {

            if (CheckOpen<T>()) {
                return null;
            }
            else {
                T win = null;

                for (int i = 0; i < unActivedWin.Count; i++) {
                    if (unActivedWin[i] is T) {
                        win = unActivedWin[i] as T;
                        break;
                    }
                }

                if (win != null) {
                    unActivedWin.Remove(win);
                }
                else {
                    win = new T();
                }

                activedWin.Add(win);
                win.Open();

                return win;
            }

        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public T Close<T> () where T : WindowViewBase {

            T win = null;

            for (int i = 0; i < activedWin.Count; i++) {
                if (activedWin[i] is T) {
                    win = activedWin[i] as T;
                    break;
                }
            }

            if (win == null) {
                Debug.Log(string.Format("该窗口已经关闭！"));
                return null;
            }
            else {
                activedWin.Remove(win);
                unActivedWin.Add(win);
                win.Close();

                return win;
            }

        }

        /// <summary>
        /// 销毁窗口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void DestroyWin<T> () where T : WindowViewBase {
            T win = null;

            for (int i = 0; i < activedWin.Count; i++) {
                if (activedWin[i] is T) {
                    win = activedWin[i] as T;
                    break;
                }
            }

            if (win == null) {
                for (int i = 0; i < unActivedWin.Count; i++) {
                    if (unActivedWin[i] is T) {
                        win = unActivedWin[i] as T;
                        break;
                    }
                }
            }

            if (win == null) {
                Debug.Log(string.Format("这个窗口并没有创建！"));
            }
            else {
                activedWin.Remove(win);
                unActivedWin.Remove(win);
                win.DoDestroy();
            }

        }

        private bool CheckOpen<T> () where T : WindowViewBase {

            for (int i = 0; i < activedWin.Count; i++) {
                if (activedWin[i] is T) {
                    Debug.Log(string.Format("<color=yellow>{0}</color>已经打开！", activedWin[i].GetType().Name));
                    return true;
                }
            }

            return false;
        }
    }

}