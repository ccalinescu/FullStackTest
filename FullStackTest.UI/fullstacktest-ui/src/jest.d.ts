/// <reference types="jest" />

import 'jest';

declare global {
  namespace jest {
    interface Expect {
      <T = any>(actual: T): jest.Matchers<void, T>;
    }
    
    interface Matchers<R, T = {}> {
      toBe(expected: T): R;
      toEqual(expected: T): R;
      toContain(expected: any): R;
      toBeTruthy(): R;
      toBeFalsy(): R;
      toHaveBeenCalled(): R;
      toHaveBeenCalledWith(...args: any[]): R;
    }
  }
}

export {};
