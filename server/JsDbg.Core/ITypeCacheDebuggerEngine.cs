﻿//--------------------------------------------------------------
//
//    MIT License
//
//    Copyright (c) Microsoft Corporation. All rights reserved.
//
//--------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JsDbg.Dia;

namespace JsDbg.Core {
    public struct SStackFrameWithContext {
        public SStackFrame StackFrame;
        public object Context;
    }

    public struct SModule {
        public string Name;
        public ulong BaseAddress;
    }

    public interface ITypeCacheDebuggerEngine {

        #region Debugger Primitives
        
        DiaSessionLoader DiaLoader { get; }

        bool IsDebuggerBusy { get; }

        bool IsPointer64Bit { get; }

        uint TargetProcess { get; set; }

        Task<uint[]> GetAttachedProcesses();

        uint TargetThread { get; set; }

        Task<uint[]> GetCurrentProcessThreads();

        Task<ulong> TebAddress();

        Task<SModule> GetModuleForAddress(ulong address);

        Task<SModule> GetModuleForName(string module);

        Task<T[]> ReadArray<T>(ulong pointer, ulong size) where T : struct;
        Task WriteValue<T>(ulong pointer, T value) where T : struct;

        Task<IEnumerable<SStackFrame>> GetCurrentCallStack(int frameCount);

        event DebuggerChangeEventHandler DebuggerChange;
        #endregion

        #region Optional Fallback Implementations

        Task<Type> GetTypeFromDebugger(string module, string typename);

        Task<SSymbolResult> LookupGlobalSymbol(string module, string symbol);

        Task<SSymbolNameAndDisplacement> LookupSymbolName(ulong pointer);

        #endregion
    }
}
