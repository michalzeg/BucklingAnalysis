export const humanize = (value: string): string => value.split(/(?=[A-Z])/).join(' ').trim();
