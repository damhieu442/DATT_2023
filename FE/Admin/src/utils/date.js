import moment from 'moment';

export const dateFormat = (date, toFormat = 'DD MMM YYYY, HH:mm A') => moment(date).format(toFormat);
export const parseDateString = (date, fromFormat) => (fromFormat ? moment(date, fromFormat) : moment(date));
