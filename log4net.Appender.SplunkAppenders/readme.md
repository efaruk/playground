log4net.Appender.SplunkAppenders

using : log4net.Appender.Extended https://www.nuget.org/packages/log4net.Appender.Extended, https://github.com/efaruk/playground/tree/master/log4net.Appender.Extended


Usage:

	<configSections>
		...
		<section name="log4net" type="log4net.Config.Log4NetConfigurationSectionHandler, log4net" />
		...
	</configSections>
	...
	<log4net debug="false">
		<appender name="TraceAppender" type="log4net.Appender.TraceAppender">
			<layout type="log4net.Layout.PatternLayout">
			<conversionPattern value="%message %exception" />
			</layout>
			<filter type="log4net.Filter.LevelRangeFilter">
			<levelMin value="ALL" />
			<levelMax value="EMERGENCY" />
			</filter>
		</appender>
		<appender name="SplunkAppender" type="log4net.Appender.SplunkAppenders.SplunkAppender, log4net.Appender.SplunkAppenders">
			<Application value="Demo" />
			<SplunkHost value="splunkenterprise" />
			<UserName value="log4net" />
			<Password value="log4netpass" />
			<IndexName value="log4net" />
			<Async value="false" />
			<EnvironmentVariablesLevel value="FATAL" />
			<layout type="log4net.Layout.PatternLayout">
			<conversionPattern value="%message %exception" />
			</layout>
			<parameter>
			<parameterName value="StackTrace" />
			<layout type="log4net.Layout.PatternLayout">
				<conversionPattern value="%stacktrace{5}" />
			</layout>
			</parameter>
			<parameter>
			<parameterName value="Exception" />
			<layout type="log4net.Layout.PatternLayout">
				<conversionPattern value="%exception" />
			</layout>
			</parameter>
			<parameter>
			<parameterName value="AspNetCache" />
			<layout type="log4net.Appender.Extended.Layout.ExtendedPatternLayout">
				<conversionPattern value="%extended-aspnet-cache{*}" />
			</layout>
			</parameter>
			<parameter>
			<parameterName value="AspNetContext" />
			<layout type="log4net.Appender.Extended.Layout.ExtendedPatternLayout">
				<conversionPattern value="%extended-aspnet-context{*}" />
			</layout>
			</parameter>
			<parameter>
			<parameterName value="AspNetRequest" />
			<layout type="log4net.Appender.Extended.Layout.ExtendedPatternLayout">
				<conversionPattern value="%extended-aspnet-request{*}" />
			</layout>
			</parameter>
			<parameter>
			<parameterName value="AspNetSession" />
			<layout type="log4net.Appender.Extended.Layout.ExtendedPatternLayout">
				<conversionPattern value="%extended-aspnet-session{*}" />
			</layout>
			</parameter>
			<filter type="log4net.Filter.LevelRangeFilter">
			<levelMin value="ALL" />
			<levelMax value="EMERGENCY" />
			</filter>
		</appender>
		<root>
			<level value="ALL" />
			<appender-ref ref="TraceAppender" />
			<appender-ref ref="SplunkAppender" />
		</root>
		<logger name="Splunk" additivity="false">
			<level value="ALL" />
			<appender-ref ref="SplunkAppender" />
		</logger>
	</log4net>
	...


Happy logging...

If your splunk entries gone too big for json please check this: https://answers.splunk.com/answers/101856/json-is-truncated.html
And create sourcetype [bigjson] copy from [_json] and add TRUNCATE=0 line below [bigjson] source type...
Also your splunk account needs those capabilities: edit_tcp, indexes_edit, request_remote_tok, rest_*, and default user capabilities

Instead of aspnet-cache, aspnet-context, aspnet-request, aspnet-session you can use wild card option with; extended-aspnet-cache{*}, extended-aspnet-context{*}, extended-aspnet-request{*}, extended-aspnet-session{*} [https://github.com/efaruk/playground/tree/master/log4net.Appender.Extended]
