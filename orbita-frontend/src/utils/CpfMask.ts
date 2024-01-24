import { Mask, type MaskOptions } from 'maska'

export const CpfMaskOptions: MaskOptions = {
  mask: '###.###.###-##',
  eager: false
}
export const CpfMask = new Mask(CpfMaskOptions)
