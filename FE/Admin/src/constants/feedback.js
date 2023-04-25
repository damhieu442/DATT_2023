import _invert from "lodash/invert";

export const EFeedbackStatus = {
	OPEN: "OPEN",
	RESOLVED: "RESOLVED",
	CLOSED: "CLOSED",
};

export const EFeedback = ["OPEN", "RESOLVED", "CLOSED"];
export const EFeedbackValueMap = _invert(EFeedback);
